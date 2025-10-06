import React from "react";
import styled from "styled-components";
import { Table as BaseTable, Btn } from "../../users/styles";
import type { Donation } from "@/domain/Donation";

const Table = styled(BaseTable)`
  color: #000;
  th, td {
    color: #000;
  }
`;

const categoryNames: Record<number, string> = {
  0: "Alimentos",
  1: "Ropa",
  2: "Dinero",
  // ...agregar más categorías según tu sistema
};

export const DonationTable: React.FC<{
  donations: Donation[];
  onEdit: (donation: Donation) => void;
  onDelete: (id: string) => void;
}> = ({ donations, onEdit, onDelete }) => {
  return (
    <Table>
      <thead>
        <tr>
          <th>Categoría</th>
          <th>Descripción</th>
          <th>Cantidad</th>
          <th>Acciones</th>
        </tr>
      </thead>
      <tbody>
        {donations?.map(donation => (
          <tr key={donation.id}>
            <td>{categoryNames[donation.category] ?? donation.category}</td>
            <td>{donation.description}</td>
            <td>{donation.quantity}</td>
            <td>
              <Btn onClick={() => onEdit(donation)} style={{background: "linear-gradient(90deg, #3252e7 0%, #ff5c6c 100%)"}}>
                Editar
              </Btn>
              <Btn onClick={() => onDelete(donation.id)} style={{background: "#eee", color: "#c41e1e"}}>
                Eliminar
              </Btn>
            </td>
          </tr>
        ))}
      </tbody>
    </Table>
  );
};