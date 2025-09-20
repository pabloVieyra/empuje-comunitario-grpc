import React from "react";
import styled from "styled-components";
import { Table as BaseTable, Btn } from "../styles";
import type { User } from "@/domain/User";

const Table = styled(BaseTable)`
  color: #000;
  th, td {
    color: #000;
  }
`;

export const UserTable: React.FC<{
  users: User[];
  onEdit: (user: User) => void;
  onDeactivate: (id: string) => void;
}> = ({ users, onEdit, onDeactivate }) => {
  return (
    <Table>
      <thead>
        <tr>
          <th>Usuario</th>
          <th>Nombre</th>
          <th>Apellido</th>
          <th>Email</th>
          <th>Teléfono</th>
          <th>Rol</th>
          <th>Estado</th>
          <th>Acciones</th>
        </tr>
      </thead>
      <tbody>
        {users.map(user => (
          <tr key={user.id}>
            <td>{user.username}</td>
            <td>{user.name}</td>
            <td>{user.lastName}</td>
            <td>{user.email}</td>
            <td>{user.phone ?? "-"}</td>
            <td>{user.role}</td>
            <td>
              <span style={{
                padding: "4px 12px",
                borderRadius: "8px",
                background: user.active ? "#e4f7e4" : "#fae4e4",
                color: user.active ? "#228b22" : "#c41e1e",
                fontWeight: 600,
              }}>
                {user.active ? "Activo" : "Inactivo"}
              </span>
            </td>
            <td>
              <Btn onClick={() => onEdit(user)} style={{background: "linear-gradient(90deg, #3252e7 0%, #ff5c6c 100%)"}}>
                Editar
              </Btn>
              {user.active && (
                <Btn onClick={() => onDeactivate(user.id)} style={{background: "#eee", color: "#c41e1e"}}>
                  Inactivar
                </Btn>
              )}
            </td>
          </tr>
        ))}
      </tbody>
    </Table>
  );
};