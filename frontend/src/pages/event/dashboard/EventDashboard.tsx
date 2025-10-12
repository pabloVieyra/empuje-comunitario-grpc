import React, { useState, useEffect } from "react";
import { CenterBox, Card, Btn, Page, Table } from "../../users/styles";
import PublicarEventoModal from "./modals/PublicarEventoModal";
import BajaEventoModal from "./modals/BajaEventoModal";
import AdhesionEventoModal from "./modals/AdhesionEventoModal";
import { getAllExternalEvents, getAllVolunteersByEvent } from "@/services/externalDataService";

export const EventDashboard: React.FC = () => {
  const [externalEvents, setExternalEvents] = useState<any[]>([]);
  const [loadingEvents, setLoadingEvents] = useState(true);
  const [errorEvents, setErrorEvents] = useState<string | null>(null);

  // Para voluntarios de un evento seleccionado
  const [volunteers, setVolunteers] = useState<any[]>([]);
  const [selectedEventVolunteers, setSelectedEventVolunteers] = useState<string | null>(null);
  const [loadingVolunteers, setLoadingVolunteers] = useState(false);

  // MODALS
  const [showPublicar, setShowPublicar] = useState(false);
  const [showBaja, setShowBaja] = useState(false);
  const [showAdhesion, setShowAdhesion] = useState(false);

  const textBlack = { color: "#222" };

  useEffect(() => {
    async function fetchEvents() {
      setLoadingEvents(true);
      setErrorEvents(null);
      try {
        const events = await getAllExternalEvents();
        setExternalEvents(Array.isArray(events) ? events : []);
      } catch (err) {
        setErrorEvents("No se pudieron cargar los eventos externos.");
      } finally {
        setLoadingEvents(false);
      }
    }
    fetchEvents();
  }, []);

  // Callback para mostrar/ocultar voluntarios de un evento
  const handleToggleVolunteers = async (eventId: string) => {
    if (selectedEventVolunteers === eventId) {
      // Si ya está abierto, ocultar
      setSelectedEventVolunteers(null);
      setVolunteers([]);
      return;
    }
    setSelectedEventVolunteers(eventId);
    setLoadingVolunteers(true);
    try {
      const vols = await getAllVolunteersByEvent(eventId);
      setVolunteers(Array.isArray(vols) ? vols : []);
    } catch {
      setVolunteers([]);
    } finally {
      setLoadingVolunteers(false);
    }
  };

  return (
    <Page>
      <CenterBox>
        <Card style={{ width: "100%", maxWidth: "1100px", alignItems: "stretch", gap: "22px" }}>
          <h2 style={{ ...textBlack, fontWeight: 700, fontSize: "2rem", textAlign: "center" }}>Dashboard Eventos</h2>
          <div
            style={{
              display: "flex",
              flexDirection: "column",
              gap: 24,
              alignItems: "center",
              justifyContent: "center",
              width: "100%",
              margin: "0 auto"
            }}
          >
            <Btn style={{ color: "#fff", width: "80%" }} onClick={() => setShowPublicar(true)}>Publicar Evento</Btn>
            <Btn style={{ background: "#e94e4e", color: "#fff", width: "80%" }} onClick={() => setShowBaja(true)}>Baja Evento</Btn>
            <Btn style={{ color: "#fff", width: "80%" }} onClick={() => setShowAdhesion(true)}>Adherirse a Evento</Btn>
          </div>
          <h3 style={{ ...textBlack, marginTop: 28, marginBottom: 6, fontWeight: 600 }}>Eventos externos</h3>
          {loadingEvents ? (
            <div style={{ color: "#222", textAlign: "center", margin: "18px 0" }}>Cargando eventos...</div>
          ) : errorEvents ? (
            <div style={{ color: "#e94e4e", textAlign: "center", margin: "18px 0" }}>{errorEvents}</div>
          ) : (
            <Table style={{ width: "100%", borderSpacing: 0, borderRadius: "18px", background: "#fff", boxShadow: "0 2px 18px #1a1c2512", overflow: "hidden", fontSize: "1.08rem" }}>
              <thead>
                <tr style={textBlack}>
                  <th style={textBlack}>Evento ID</th>
                  <th style={textBlack}>Organización</th>
                  <th style={textBlack}>Nombre</th>
                  <th style={textBlack}>Descripción</th>
                  <th style={textBlack}>Fecha</th>
                  <th style={textBlack}>Ver Voluntarios</th>
                </tr>
              </thead>
              <tbody>
                {externalEvents.length === 0 ? (
                  <tr>
                    <td colSpan={6} style={{ ...textBlack, textAlign: "center" }}>No hay eventos externos</td>
                  </tr>
                ) : (
                  externalEvents.map(ev => (
                    <tr key={ev.eventId || ev.id} style={textBlack}>
                      <td style={textBlack}>{ev.eventId || ev.id}</td>
                      <td style={textBlack}>{ev.orgId || ev.organization}</td>
                      <td style={textBlack}>{ev.nameEvent || ev.name}</td>
                      <td style={textBlack}>{ev.description}</td>
                      <td style={textBlack}>{ev.dateTimeEvent || ev.date}</td>
                      <td>
                        <Btn
                          style={{
                            background: selectedEventVolunteers === (ev.eventId || ev.id)
                              ? "#e94e4e"
                              : "#2456e9",
                            color: "#fff",
                            padding: "7px 16px",
                            fontSize: "0.95rem"
                          }}
                          onClick={() => handleToggleVolunteers(ev.eventId || ev.id)}
                        >
                          {selectedEventVolunteers === (ev.eventId || ev.id) ? "Ocultar" : "Ver voluntarios"}
                        </Btn>
                      </td>
                    </tr>
                  ))
                )}
              </tbody>
            </Table>
          )}
          {selectedEventVolunteers && (
            <>
              <h3 style={{ ...textBlack, marginTop: 28, marginBottom: 6, fontWeight: 600 }}>
                Voluntarios del evento <span style={{ fontWeight: 400 }}>{selectedEventVolunteers}</span>
              </h3>
              {loadingVolunteers ? (
                <div style={{ color: "#222", textAlign: "center", margin: "18px 0" }}>Cargando voluntarios...</div>
              ) : (
                <Table style={{ width: "100%", borderSpacing: 0, borderRadius: "18px", background: "#fff", boxShadow: "0 2px 18px #1a1c2512", overflow: "hidden", fontSize: "1.08rem" }}>
                  <thead>
                    <tr style={textBlack}>
                      <th style={textBlack}>Voluntario ID</th>
                      <th style={textBlack}>Nombre</th>
                      <th style={textBlack}>Apellido</th>
                      <th style={textBlack}>Teléfono</th>
                      <th style={textBlack}>Email</th>
                    </tr>
                  </thead>
                  <tbody>
                    {volunteers.length === 0 ? (
                      <tr>
                        <td colSpan={5} style={{ ...textBlack, textAlign: "center" }}>No hay voluntarios para este evento</td>
                      </tr>
                    ) : (
                      volunteers.map(vol => (
                        <tr key={vol.volunteerId || vol.id} style={textBlack}>
                          <td style={textBlack}>{vol.volunteerId || vol.id}</td>
                          <td style={textBlack}>{vol.name}</td>
                          <td style={textBlack}>{vol.lastName}</td>
                          <td style={textBlack}>{vol.phone}</td>
                          <td style={textBlack}>{vol.email}</td>
                        </tr>
                      ))
                    )}
                  </tbody>
                </Table>
              )}
            </>
          )}
          {showPublicar && <PublicarEventoModal onClose={() => setShowPublicar(false)} />}
          {showBaja && <BajaEventoModal onClose={() => setShowBaja(false)} events={externalEvents} />}
          {showAdhesion && <AdhesionEventoModal onClose={() => setShowAdhesion(false)} events={externalEvents} />}
        </Card>
      </CenterBox>
    </Page>
  );
};

export default EventDashboard;