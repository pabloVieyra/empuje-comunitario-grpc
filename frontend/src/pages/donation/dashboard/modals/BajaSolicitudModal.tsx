import React, { useState } from "react";
import { ModalBg, ModalCard, ModalTitle, ModalFooter, InputWrapper, Label, Btn, Select } from "../../../users/styles";
import { cancelRequestDonation } from "../../../../services/MessageService";

const ORG_ID = "3fa85f64-5717-4562-b3fc-2c963f66afa6";

const BajaSolicitudModal: React.FC<{ onClose: () => void, requests: any[] }> = ({ onClose, requests }) => {
  const [selected, setSelected] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    if (!selected) {
      setError("Selecciona una solicitud.");
      return;
    }
    setLoading(true);

    try {
      await cancelRequestDonation({
        requestOrgId: ORG_ID,
        requestId: selected
      });
      onClose();
    } catch (err: any) {
      setError("Error al dar de baja la solicitud. Intenta nuevamente.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <ModalBg>
      <ModalCard>
        <ModalTitle style={{ color: "#222" }}>Baja Solicitud</ModalTitle>
        <form onSubmit={handleSubmit} autoComplete="off">
          <InputWrapper>
            <Label style={{ color: "#222" }}>Seleccionar solicitud</Label>
            <Select value={selected} onChange={e => setSelected(e.target.value)}>
              <option value="">Seleccionar</option>
              {requests.map(r =>
                <option key={r.requestId} value={r.requestId}>{r?.donations[0]?.category}: {r?.donations[0]?.description}</option>
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

export default BajaSolicitudModal;