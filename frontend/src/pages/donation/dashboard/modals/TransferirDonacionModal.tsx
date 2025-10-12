import React, { useState } from "react";
import { ModalBg, ModalCard, ModalTitle, ModalFooter, InputWrapper, Label, Input, Btn, Select } from "../../../users/styles";
import { transferDonation } from "../../../../services/MessageService";

const DONATION_ORG_ID = "3fa85f64-5717-4562-b3fc-2c963f66afa6";

const TransferirDonacionModal: React.FC<{ onClose: () => void, requests: any[] }> = ({ onClose, requests }) => {
  const [solicitudId, setSolicitudId] = useState("");
  const [categoria, setCategoria] = useState("");
  const [descripcion, setDescripcion] = useState("");
  const [cantidad, setCantidad] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    if (!solicitudId || !categoria || !descripcion || !cantidad) {
      setError("Completa todos los campos.");
      return;
    }
    setLoading(true);

    try {
      await transferDonation(
        DONATION_ORG_ID,
        {
          requestId: solicitudId,
          donationOrgId: DONATION_ORG_ID,
          donations: [{ category: categoria, description: descripcion, quantity: Number(cantidad) }]
        }
      );
      onClose();
    } catch (err: any) {
      setError("Error al transferir donación. Intenta nuevamente.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <ModalBg>
      <ModalCard>
        <ModalTitle style={{ color: "#222" }}>Transferir Donación</ModalTitle>
        <form onSubmit={handleSubmit} autoComplete="off">
          <InputWrapper>
            <Label style={{ color: "#222" }}>Solicitud externa</Label>
            <Select value={solicitudId} onChange={e => setSolicitudId(e.target.value)}>
              <option value="">Seleccionar</option>
              {requests.map(r => <option key={r.requestId} value={r.requestId}>{r?.donations[0]?.category}: {r?.donations[0]?.description}</option>)}
            </Select>
          </InputWrapper>
          <InputWrapper>
            <Label style={{ color: "#222" }}>Categoría</Label>
            <Input value={categoria} onChange={e => setCategoria(e.target.value)} placeholder="Ej: Alimentos" style={{ color: "#222" }} />
          </InputWrapper>
          <InputWrapper>
            <Label style={{ color: "#222" }}>Descripción</Label>
            <Input value={descripcion} onChange={e => setDescripcion(e.target.value)} placeholder="Ej: Puré de tomates" style={{ color: "#222" }} />
          </InputWrapper>
          <InputWrapper>
            <Label style={{ color: "#222" }}>Cantidad</Label>
            <Input value={cantidad} onChange={e => setCantidad(e.target.value)} placeholder="Ej: 2" style={{ color: "#222" }} />
          </InputWrapper>
          {error && <div style={{ color: "#e94e4e", marginBottom: 10, textAlign: "center" }}>{error}</div>}
          <ModalFooter>
            <Btn type="submit" disabled={loading}>{loading ? "Transfiriendo..." : "Transferir"}</Btn>
            <Btn type="button" style={{ background: "#aaa" }} onClick={onClose}>Cancelar</Btn>
          </ModalFooter>
        </form>
      </ModalCard>
    </ModalBg>
  );
};

export default TransferirDonacionModal;