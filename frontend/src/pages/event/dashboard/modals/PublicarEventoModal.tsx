import React, { useState } from "react";
import { ModalBg, ModalCard, ModalTitle, ModalFooter, InputWrapper, Label, Input, Btn } from "../../../users/styles";
import { v4 as uuidv4 } from "uuid";
import { createSolidaryEvent } from "@/services/MessageService";

// Puedes pasar orgId como prop si tienes el contexto del usuario logueado
const ORG_ID = "3fa85f64-5717-4562-b3fc-2c963f66afa6";

const PublicarEventoModal: React.FC<{ onClose: () => void }> = ({ onClose }) => {
  const [nombre, setNombre] = useState("");
  const [descripcion, setDescripcion] = useState("");
  const [fecha, setFecha] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    if (!nombre || !descripcion || !fecha) {
      setError("Completa todos los campos.");
      return;
    }
    setLoading(true);

    try {
      const eventId = uuidv4();
      const dateObj = new Date(fecha);
      const utcDateString = dateObj.toISOString(); // Ejemplo: "2025-10-23T19:39:00.000Z"

      await createSolidaryEvent({
        orgId: ORG_ID,
        eventId,
        nameEvent: nombre,
        description: descripcion,
        dateTimeEvent: utcDateString
      });
      onClose();
    } catch (err: any) {
      setError("Error al publicar evento. Intenta nuevamente.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <ModalBg>
      <ModalCard>
        <ModalTitle style={{ color: "#222" }}>Publicar Evento</ModalTitle>
        <form onSubmit={handleSubmit} autoComplete="off">
          <InputWrapper>
            <Label style={{ color: "#222" }}>Nombre del evento</Label>
            <Input value={nombre} onChange={e => setNombre(e.target.value)} placeholder="Ej: Jornada Solidaria" style={{ color: "#222" }} />
          </InputWrapper>
          <InputWrapper>
            <Label style={{ color: "#222" }}>Descripción</Label>
            <Input value={descripcion} onChange={e => setDescripcion(e.target.value)} placeholder="Ej: Evento de ayuda" style={{ color: "#222" }} />
          </InputWrapper>
          <InputWrapper>
            <Label style={{ color: "#222" }}>Fecha y hora</Label>
            <Input type="datetime-local" value={fecha} onChange={e => setFecha(e.target.value)} style={{ color: "#222" }} />
          </InputWrapper>
          {error && <div style={{ color: "#e94e4e", marginBottom: 10, textAlign: "center" }}>{error}</div>}
          <ModalFooter>
            <Btn type="submit" disabled={loading}>{loading ? "Publicando..." : "Publicar"}</Btn>
            <Btn type="button" style={{ background: "#aaa" }} onClick={onClose}>Cancelar</Btn>
          </ModalFooter>
        </form>
      </ModalCard>
    </ModalBg>
  );
};

export default PublicarEventoModal;