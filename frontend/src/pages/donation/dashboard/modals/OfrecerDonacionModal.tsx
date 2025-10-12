import React, { useState } from "react";
import { ModalBg, ModalCard, ModalTitle, ModalFooter, InputWrapper, Label, Input, Btn } from "../../../users/styles";
import { offerDonations } from "../../../../services/MessageService";
import { v4 as uuidv4 } from "uuid";

const DONATION_ORG_ID = "3fa85f64-5717-4562-b3fc-2c963f66afa6";

const OfrecerDonacionModal: React.FC<{ onClose: () => void }> = ({ onClose }) => {
  const [categoria, setCategoria] = useState("");
  const [descripcion, setDescripcion] = useState("");
  const [cantidad, setCantidad] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    if (!categoria || !descripcion || !cantidad) {
      setError("Completa todos los campos.");
      return;
    }
    setLoading(true);

    try {
      await offerDonations({
        offerId: uuidv4(),
        donationOrganizationId: DONATION_ORG_ID,
        donations: [{ category: categoria, description: descripcion, quantity: Number(cantidad) }]
      });
      onClose();
    } catch (err: any) {
      setError("Error al ofrecer donación. Intenta nuevamente.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <ModalBg>
      <ModalCard>
        <ModalTitle style={{ color: "#222" }}>Ofrecer Donación</ModalTitle>
        <form onSubmit={handleSubmit} autoComplete="off">
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
            <Btn type="submit" disabled={loading}>{loading ? "Ofreciendo..." : "Ofrecer"}</Btn>
            <Btn type="button" style={{ background: "#aaa" }} onClick={onClose}>Cancelar</Btn>
          </ModalFooter>
        </form>
      </ModalCard>
    </ModalBg>
  );
};

export default OfrecerDonacionModal;