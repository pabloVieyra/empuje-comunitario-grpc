import React, { useState } from "react";
import { ModalBg, ModalCard, ModalTitle, ModalFooter, InputWrapper, Label, Select, Input, Btn } from "../../../users/styles";
import { notifyEventVolunteer } from "../../../../services/messageService";
import { v4 as uuidv4 } from "uuid";

// Puedes pasar orgId como prop si tienes el contexto del usuario logueado
const ORG_ID = "3fa85f64-5717-4562-b3fc-2c963f66afa6";

const AdhesionEventoModal: React.FC<{ onClose: () => void, events: any[] }> = ({ onClose, events }) => {
  console.log("Events in AdhesionEventoModal:", events);
  const [eventId, setEventId] = useState("");
  const [nombre, setNombre] = useState("");
  const [apellido, setApellido] = useState("");
  const [telefono, setTelefono] = useState("");
  const [email, setEmail] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    if (!eventId || !nombre || !apellido || !telefono || !email) {
      setError("Completa todos los campos.");
      return;
    }
    setLoading(true);
  

    try {
      await notifyEventVolunteer(
        ORG_ID,
        {
          eventId,
          orgId: ORG_ID,
          volunteerId: uuidv4(),
          name: nombre,
          lastName: apellido,
          phone: telefono,
          email
        }
      );
      onClose();
    } catch (err: any) {
      setError("Error al adherirse al evento. Intenta nuevamente.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <ModalBg>
      <ModalCard>
        <ModalTitle style={{ color: "#222" }}>Adherirse a Evento</ModalTitle>
        <form onSubmit={handleSubmit} autoComplete="off">
          <InputWrapper>
            <Label style={{ color: "#222" }}>Evento externo</Label>
            <Select value={eventId} onChange={e => setEventId(e.target.value)}>
              <option value="">Seleccionar</option>
              {events.map(ev =>
                <option key={ev.id} value={ev.eventId}>{ev?.nameEvent} - {ev?.description}</option>
              )}
            </Select>
          </InputWrapper>
          <InputWrapper>
            <Label style={{ color: "#222" }}>Nombre</Label>
            <Input value={nombre} onChange={e => setNombre(e.target.value)} style={{ color: "#222" }} />
          </InputWrapper>
          <InputWrapper>
            <Label style={{ color: "#222" }}>Apellido</Label>
            <Input value={apellido} onChange={e => setApellido(e.target.value)} style={{ color: "#222" }} />
          </InputWrapper>
          <InputWrapper>
            <Label style={{ color: "#222" }}>Teléfono</Label>
            <Input value={telefono} onChange={e => setTelefono(e.target.value)} style={{ color: "#222" }} />
          </InputWrapper>
          <InputWrapper>
            <Label style={{ color: "#222" }}>Email</Label>
            <Input type="email" value={email} onChange={e => setEmail(e.target.value)} style={{ color: "#222" }} />
          </InputWrapper>
          {error && <div style={{ color: "#e94e4e", marginBottom: 10, textAlign: "center" }}>{error}</div>}
          <ModalFooter>
            <Btn type="submit" disabled={loading}>{loading ? "Adhiriendo..." : "Adherirse"}</Btn>
            <Btn type="button" style={{ background: "#aaa" }} onClick={onClose}>Cancelar</Btn>
          </ModalFooter>
        </form>
      </ModalCard>
    </ModalBg>
  );
};

export default AdhesionEventoModal;