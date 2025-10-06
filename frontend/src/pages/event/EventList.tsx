import React, { useState } from "react";
import { useEvents } from "@/hooks/useEvents";
import { Btn, Card, ErrorMsg, UsersPage } from "../styles";
import { EventTable } from "./EventTable";
import { EventModal } from "./EventModal";

export const EventList: React.FC = () => {
  const { events, loading, error, addEvent, editEvent, removeEvent, setError } = useEvents();
  const [modalOpen, setModalOpen] = useState(false);
  const [editEventData, setEditEventData] = useState<any>(null);

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
      await editEvent({ eventId: id, ...data });
    } else {
      await addEvent(data);
    }
    setModalOpen(false);
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
          onDelete={(eventId: number) => removeEvent(eventId, actorId)}
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
    </UsersPage>
  );
};