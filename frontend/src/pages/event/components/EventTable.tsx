import React from "react";
import styled from "styled-components";
import { Table as BaseTable, Btn } from "../../users/styles";
import type { Event } from "@/domain/Event";

const Table = styled(BaseTable)`
  color: #000;
  th, td {
    color: #000;
  }
`;

export const EventTable: React.FC<{
  events: Event[];
  onEdit: (event: Event) => void;
  onDelete: (id: number) => void;
  onManageMembers: (event: Event) => void;
  onManageDonations: (event: Event) => void;
}> = ({ events, onEdit, onDelete, onManageMembers, onManageDonations }) => {
  const isPastEvent = (event: Event) => {
    const eventDate = new Date(event.eventDateTime);
    const now = new Date();
    const eventDay = Date.UTC(eventDate.getUTCFullYear(), eventDate.getUTCMonth(), eventDate.getUTCDate());
    const nowDay = Date.UTC(now.getUTCFullYear(), now.getUTCMonth(), now.getUTCDate());
    return eventDay <= nowDay;
  };

  events.map(event => console.log(isPastEvent(event)));

  return (
    <Table>
      <thead>
        <tr>
          <th>Nombre</th>
          <th>Descripción</th>
          <th>Fecha</th>
          <th>Acciones</th>
        </tr>
      </thead>
      <tbody>
        {events.map(event => (
          <tr key={event.id}>
            <td>{event.eventName}</td>
            <td>{event.description}</td>
            <td>{new Date(event.eventDateTime).toLocaleString()}</td>
            <td>
              <Btn onClick={() => onEdit(event)} style={{background: "linear-gradient(90deg, #3252e7 0%, #ff5c6c 100%)"}}>
                Editar
              </Btn>
              <Btn onClick={() => onDelete(event.id)} style={{background: "#eee", color: "#c41e1e"}}>
                Eliminar
              </Btn>
              <Btn onClick={() => onManageMembers(event)} style={{background: "#19c27c", color: "#fff"}}>
                Gestionar Participantes
              </Btn>
              {isPastEvent(event) && (
                <Btn onClick={() => onManageDonations(event)} style={{background: "#f9c846", color: "#23244b"}}>
                  Registrar Donaciones
                </Btn>
              )}
            </td>
          </tr>
        ))}
      </tbody>
    </Table>
  );
};