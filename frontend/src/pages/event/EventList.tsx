import React, { useState } from "react";
import { useEvents } from "./useEvent";
import { Btn, Card, ErrorMsg, UsersPage } from "../users/styles";
import { EventModal } from "./components/EventModal";
import { EventTable } from "./components/EventTable";
import { EventMembersModal } from "./components/EventMembersModal";
import { EventDonationsModal } from "./components/EventDonationsModal";

export const EventList: React.FC = () => {
  const { events, loading, error, addEvent, editEvent, removeEvent, setError, fetchEvents } = useEvents();
  const [modalOpen, setModalOpen] = useState(false);
  const [editEventData, setEditEventData] = useState<any>(null);
  const [membersModalOpen, setMembersModalOpen] = useState(false);
  const [donationsModalOpen, setDonationsModalOpen] = useState(false);
  const [selectedEvent, setSelectedEvent] = useState<Event | null>(null);

  const openMembersModal = (event: Event) => {
    setSelectedEvent(event);
    setMembersModalOpen(true);
  };

  const openDonationsModal = (event: Event) => {
    console.log("Opening donations modal for event:", event);
    setSelectedEvent(event);
    setDonationsModalOpen(true);
  };

  const openNewModal = () => {
    setEditEventData(null);
    setModalOpen(true);
    setError(null);
  };

  const openEditModal = (event: any) => {
    setEditEventData(event);
    setModalOpen(true);
    setError(null);
  };

  const handleSave = async (id: number | null, data: any) => {
    if (id) {
      await editEvent({ id: id, ...data });
    } else {
      await addEvent(data);
    }
    setModalOpen(false);
    fetchEvents();
  };

  // actorId debería ser el usuario logueado, este es un ejemplo:
  const actorId = "actorIdEjemplo";

  return (
    <UsersPage>
      <Card>
        <div style={{display: "flex", justifyContent: "space-between", alignItems: "center", flexDirection: "column", marginBottom: 18}}>
          <h2 style={{
            fontWeight: 700,
            fontSize: "2.2rem",
            color: "#23244b",
            letterSpacing: "0.01em",
          }}>
            ABM de Eventos
          </h2>
          <Btn onClick={openNewModal}>+ Nuevo Evento</Btn>
        </div>
        {error && <ErrorMsg>{error}</ErrorMsg>}
        {loading && <div style={{margin:'18px 0'}}>Cargando...</div>}
        <EventTable
          events={events}
          onEdit={openEditModal}
          onDelete={(id: number) => removeEvent(id, actorId)}
          onManageMembers={openMembersModal}
          onManageDonations={openDonationsModal}
        />
      </Card>
      {modalOpen && (
        <EventModal
          event={editEventData}
          onClose={() => setModalOpen(false)}
          onSave={handleSave}
          error={error}
        />
      )}
      {membersModalOpen && selectedEvent && (
        <EventMembersModal
          event={selectedEvent}
          onClose={() => setMembersModalOpen(false)}
          onRefresh={fetchEvents}
        />
      )}
      {donationsModalOpen && selectedEvent && (
        <EventDonationsModal
          event={selectedEvent}
          onClose={() => setDonationsModalOpen(false)}
          onRefresh={fetchEvents}
        />
      )}
    </UsersPage>
  );
};