import React, { useState } from "react";
import { ModalBg, ModalCard, ModalTitle, ModalFooter, Btn, ErrorMsg } from "../styles";
import type { User, UserRole } from "@/domain/User";

const roles: UserRole[] = ["ADMIN", "PRESIDENT", "USER"];

export const UserModal: React.FC<{
  user?: User;
  onClose: () => void;
  onSave: (id: string | null, data: any) => void;
  error?: string | null;
}> = ({ user, onClose, onSave, error }) => {
  const [form, setForm] = useState({
    username: user?.username ?? "",
    name: user?.name ?? "",
    lastName: user?.lastName ?? "",
    phone: user?.phone ?? "",
    email: user?.email ?? "",
    role: user?.role ?? "USER",
    active: user?.active ?? true,
  });



const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
  const { name, value } = e.target;
  const checked = e.target instanceof HTMLInputElement ? e.target.checked : undefined;
  
  setForm(f => ({
    ...f,
      [name]: e.target.type === "checkbox" ? checked : value,
  }));
};

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!form.username || !form.name || !form.lastName || !form.email) return;
    onSave(user ? user.id : null, form);
  };

  return (
    <ModalBg>
      <ModalCard>
        <ModalTitle>{user ? "Editar Usuario" : "Nuevo Usuario"}</ModalTitle>
        <form onSubmit={handleSubmit} autoComplete="off">
          <label>Usuario *</label>
          <input
            name="username"
            value={form.username}
            onChange={handleChange}
            required
            disabled={!!user}
            style={{marginBottom: 12, width: '100%', padding: '10px'}}
          />
          <label>Nombre *</label>
          <input
            name="name"
            value={form.name}
            onChange={handleChange}
            required
            style={{marginBottom: 12, width: '100%', padding: '10px'}}
          />
          <label>Apellido *</label>
          <input
            name="lastName"
            value={form.lastName}
            onChange={handleChange}
            required
            style={{marginBottom: 12, width: '100%', padding: '10px'}}
          />
          <label>Teléfono</label>
          <input
            name="phone"
            value={form.phone}
            onChange={handleChange}
            style={{marginBottom: 12, width: '100%', padding: '10px'}}
          />
          <label>Email *</label>
          <input
            name="email"
            value={form.email}
            onChange={handleChange}
            required
            disabled={!!user}
            type="email"
            style={{marginBottom: 12, width: '100%', padding: '10px'}}
          />
          <label>Rol</label>
          <select
            name="role"
            value={form.role}
            onChange={handleChange}
            style={{marginBottom: 12, width: '100%', padding: '10px'}}
          >
            {roles.map(r => <option key={r} value={r}>{r}</option>)}
          </select>
          {user && (
            <label style={{display: 'flex', alignItems: 'center', marginBottom: 12}}>
              <input
                type="checkbox"
                name="activo"
                checked={form.active}
                onChange={handleChange}
                style={{marginRight: 8}}
              />
              Activo
            </label>
          )}
          {error && <ErrorMsg>{error}</ErrorMsg>}
          <ModalFooter>
            <Btn type="submit">{user ? "Guardar cambios" : "Crear usuario"}</Btn>
            <Btn type="button" onClick={onClose} style={{background: "#aaa"}}>Cancelar</Btn>
          </ModalFooter>
        </form>
      </ModalCard>
    </ModalBg>
  );
};