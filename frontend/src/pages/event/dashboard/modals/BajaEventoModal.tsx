import React, { useState } from "react";
import { ModalBg, ModalCard, ModalTitle, ModalFooter, InputWrapper, Label, Select, Btn } from "../../../users/styles";
import { cancelSolidaryEvent } from "@/services/MessageService";

const ORG_ID = "3fa85f64-5717-4562-b3fc-2c963f66afa6";

const BajaEventoModal: React.FC<{ onClose: () => void, events: any[] }> = ({ onClose, events }) => {
  const [selected, setSelected] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    if (!selected) {
      setError("Selecciona un evento.");
      return;
    }
    setLoading(true);

    try {
      console.log("Cancelling event with ID:", selected);
      await cancelSolidaryEvent({
        orgId: ORG_ID,
        eventId: selected
      });
      onClose();
    } catch (err: any) {
      setError("Error al dar de baja el evento. Intenta nuevamente.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <ModalBg>
      <ModalCard>
        <ModalTitle style={{ color: "#222" }}>Baja Evento</ModalTitle>
        <form onSubmit={handleSubmit} autoComplete="off">
          <InputWrapper>
            <Label style={{ color: "#222" }}>Seleccionar evento</Label>
            <Select value={selected} onChange={e => { 
              console.log("Selected event ID:", e.target.value);
              setSelected(e.target.value); 
              }}>
              <option value="">Seleccionar</option>
              {events.map(ev =>
                <option key={ev.eventId} value={ev.eventId}>{ev.nameEvent}: {ev.description}</option>
              )}
            </Select>
          </InputWrapper>
          {error && <div style={{ color: "#e94e4e", marginBottom: 10, textAlign: "center" }}>{error}</div>}
          <ModalFooter>
            <Btn type="submit" style={{ background: "#e94e4e" }} disabled={loading}>{loading ? "Dando de baja..." : "Dar de Baja"}</Btn>
            <Btn type="button" style={{ background: "#aaa" }} onClick={onClose}>Cancelar</Btn>
          </ModalFooter>
        </form>
      </ModalCard>
    </ModalBg>
  );
};

export default BajaEventoModal;