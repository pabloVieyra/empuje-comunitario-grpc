import React from "react";
import styled from "styled-components";
import { Table as BaseTable, Btn } from "../styles";
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
  onDelete: (eventId: number) => void;
}> = ({ events, onEdit, onDelete }) => {
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
          <tr key={event.eventId}>
            <td>{event.eventName}</td>
            <td>{event.description}</td>
            <td>{new Date(event.eventDateTime).toLocaleString()}</td>
            <td>
              <Btn onClick={() => onEdit(event)} style={{background: "linear-gradient(90deg, #3252e7 0%, #ff5c6c 100%)"}}>
                Editar
              </Btn>
              <Btn onClick={() => onDelete(event.eventId)} style={{background: "#eee", color: "#c41e1e"}}>
                Eliminar
              </Btn>
            </td>
          </tr>
        ))}
      </tbody>
    </Table>
  );
};