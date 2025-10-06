import React, { useState, useEffect } from "react";
import { ModalBg, ModalCard, ModalTitle, ModalFooter, Input, ErrorMsg, Label, InputWrapper, Btn } from "../styles";
import type { Event } from "@/domain/Event";

interface Props {
  event: Event | null;
  onClose: () => void;
  onSave: (id: number | null, data: Omit<Event, "eventId">) => void;
  error: string | null;
}

export const EventModal: React.FC<Props> = ({ event, onClose, onSave, error }) => {
  const [eventName, setEventName] = useState<string>("");
  const [description, setDescription] = useState<string>("");
  const [eventDateTime, setEventDateTime] = useState<string>("");
  const [creationUserId, setCreationUserId] = useState<string>("");

  useEffect(() => {
    if (event) {
      setEventName(event.eventName);
      setDescription(event.description);
      setEventDateTime(event.eventDateTime.slice(0, 16)); // for input type="datetime-local"
      setCreationUserId(event.creationUserId);
    } else {
      setEventName("");
      setDescription("");
      setEventDateTime("");
      setCreationUserId(""); // O setear al user logueado si corresponde
    }
  }, [event]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSave(event ? event.eventId : null, {
      eventName,
      description,
      eventDateTime: new Date(eventDateTime).toISOString(),
      creationUserId,
    });
  };

  return (
    <ModalBg>
      <ModalCard>
        <ModalTitle>{event ? "Editar Evento" : "Nuevo Evento"}</ModalTitle>
        <form onSubmit={handleSubmit}>
          <InputWrapper>
            <Label>Nombre</Label>
            <Input value={eventName} onChange={e => setEventName(e.target.value)} required />
          </InputWrapper>
          <InputWrapper>
            <Label>Descripción</Label>
            <Input value={description} onChange={e => setDescription(e.target.value)} required />
          </InputWrapper>
          <InputWrapper>
            <Label>Fecha y Hora</Label>
            <Input
              type="datetime-local"
              value={eventDateTime}
              onChange={e => setEventDateTime(e.target.value)}
              required
            />
          </InputWrapper>
          <InputWrapper>
            <Label>Usuario Creador (ID)</Label>
            <Input value={creationUserId} onChange={e => setCreationUserId(e.target.value)} required />
          </InputWrapper>
          {error && <ErrorMsg>{error}</ErrorMsg>}
          <ModalFooter>
            <Btn type="button" onClick={onClose} style={{background: "#eee", color: "#234"}}>Cancelar</Btn>
            <Btn type="submit">{event ? "Guardar Cambios" : "Crear Evento"}</Btn>
          </ModalFooter>
        </form>
      </ModalCard>
    </ModalBg>
  );
}; 