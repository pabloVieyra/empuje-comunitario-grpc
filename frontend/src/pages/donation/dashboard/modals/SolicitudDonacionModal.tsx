import React, { useState } from "react";
import { ModalBg, ModalCard, ModalTitle, ModalFooter, InputWrapper, Label, Input, Btn,  } from "../../../users/styles";
// Update the import path if necessary, or create the file if missing
import { requestDonation } from "../../../../services/MessageService";

// Puedes pasar organizationId como prop si tienes el contexto de usuario logueado
const ORGANIZATION_ID = "3fa85f64-5717-4562-b3fc-2c963f66afa6"; // Reemplaza por el real o pásalo como prop

const SolicitudDonacionModal: React.FC<{ onClose: () => void }> = ({ onClose }) => {
  const [categoria, setCategoria] = useState("");
  const [descripcion, setDescripcion] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    if (!categoria || !descripcion) {
      setError("Completa todos los campos.");
      return;
    }
    setLoading(true);

    try {
      const requestId = crypto.randomUUID(); // Genera un ID único
      await requestDonation({
        requesterOrgId: ORGANIZATION_ID,
        requestId,
        donations: [
          { category: categoria, description: descripcion, quantity: 1 }
        ]
      });
      onClose();
    } catch (err: any) {
      setError("Error al solicitar donación. Intenta nuevamente.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <ModalBg>
      <ModalCard>
        <ModalTitle style={{ color: "#222" }}>Solicitar Donación</ModalTitle>
        <form onSubmit={handleSubmit} autoComplete="off">
          <InputWrapper>
            <Label style={{ color: "#222" }}>Categoría</Label>
            <Input
              style={{ color: "#222" }}
              value={categoria}
              onChange={e => setCategoria(e.target.value)}
              placeholder="Ej: Alimentos"
            />
          </InputWrapper>
          <InputWrapper>
            <Label style={{ color: "#222" }}>Descripción</Label>
            <Input
              style={{ color: "#222" }}
              value={descripcion}
              onChange={e => setDescripcion(e.target.value)}
              placeholder="Ej: Puré de tomates"
            />
          </InputWrapper>
          {error && <div style={{ color: "#e94e4e", marginBottom: 10, textAlign: "center" }}>{error}</div>}
          <ModalFooter>
            <Btn type="submit" disabled={loading}>{loading ? "Solicitando..." : "Solicitar"}</Btn>
            <Btn type="button" style={{ background: "#aaa" }} onClick={onClose}>Cancelar</Btn>
          </ModalFooter>
        </form>
      </ModalCard>
    </ModalBg>
  );
};

export default SolicitudDonacionModal;

function uuidv4() {
  throw new Error("Function not implemented.");
}
