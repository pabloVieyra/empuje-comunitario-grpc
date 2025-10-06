import React, { useState, useEffect } from "react";
import { ModalBg, ModalCard, ModalTitle, ModalFooter, Input, Select, ErrorMsg, Label, InputWrapper, Btn } from "../../users/styles";
import type { Donation } from "@/domain/Donation";

const categoryOptions = [
  { value: 0, label: "Alimentos" },
  { value: 1, label: "Ropa" },
  { value: 2, label: "Dinero" },
];

interface Props {
  donation: Donation | null;
  onClose: () => void;
  onSave: (id: string | null, data: Omit<Donation, "id">) => void;
  error: string | null;
}

export const DonationModal: React.FC<Props> = ({ donation, onClose, onSave, error }) => {
  const [category, setCategory] = useState<number>(0);
  const [description, setDescription] = useState<string>("");
  const [quantity, setQuantity] = useState<number>(1);

  useEffect(() => {
    if (donation) {
      setCategory(donation.category);
      setDescription(donation.description);
      setQuantity(donation.quantity);
    } else {
      setCategory(0);
      setDescription("");
      setQuantity(1);
    }
  }, [donation]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSave(donation ? donation.id : null, { category, description, quantity });
  };

  return (
    <ModalBg>
      <ModalCard>
        <ModalTitle>{donation ? "Editar Donación" : "Nueva Donación"}</ModalTitle>
        <form onSubmit={handleSubmit}>
          <InputWrapper>
            <Label>Categoría</Label>
            <Select value={category} color="black" onChange={e => setCategory(Number(e.target.value))}>
              {categoryOptions.map(opt => (
                <option key={opt.value} value={opt.value}>{opt.label}</option>
              ))}
            </Select>
          </InputWrapper>
          <InputWrapper>
            <Label>Descripción</Label>
            <Input value={description} color={"black"}
              onChange={e => setDescription(e.target.value)} required />
          </InputWrapper>
          <InputWrapper>
            <Label>Cantidad</Label>
            <Input type="number" min={1} color={"black"}
              value={quantity} onChange={e => setQuantity(Number(e.target.value))} required />
          </InputWrapper>
          {error && <ErrorMsg>{error}</ErrorMsg>}
          <ModalFooter>
            <Btn type="button" onClick={onClose} style={{ background: "#eee", color: "#234" }}>Cancelar</Btn>
            <Btn type="submit">{donation ? "Guardar Cambios" : "Crear Donación"}</Btn>
          </ModalFooter>
        </form>
      </ModalCard>
    </ModalBg>
  );
};