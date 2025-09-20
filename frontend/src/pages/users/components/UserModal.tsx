import React, { useState } from "react";
import { ModalBg, ModalCard, ModalTitle, ModalFooter, Btn, ErrorMsg, InputWrapper, Label } from "../styles";
import type { User, UserRole } from "@/domain/User";
import { Input } from "@chakra-ui/react";

const roles: UserRole[] = ["ADMIN", "PRESIDENT", "VOCAL", "COORDINATOR", "VOLUNTEER"];

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
    const { name, value, type, checked } = e.target;
    setForm(f => ({
      ...f,
      [name]: type === "checkbox" ? checked : value,
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
          <InputWrapper>
            <Label htmlFor="username">Usuario *</Label>
            <Input
              color={"black"}
              id="username"
              name="username"
              value={form.username}
              onChange={handleChange}
              required
              disabled={!!user}
            />
          </InputWrapper>
          <InputWrapper>
            <Label htmlFor="name">Nombre *</Label>
            <Input
              color={"black"}
              id="name"
              name="name"
              value={form.name}
              onChange={handleChange}
              required
            />
          </InputWrapper>
          <InputWrapper>
            <Label htmlFor="lastName">Apellido *</Label>
            <Input
              color={"black"}
              id="lastName"
              name="lastName"
              value={form.lastName}
              onChange={handleChange}
              required
            />
          </InputWrapper>
          <InputWrapper>
            <Label htmlFor="phone">Teléfono</Label>
            <Input
              color={"black"}
              id="phone"
              name="phone"
              value={form.phone}
              onChange={handleChange}
            />
          </InputWrapper>
          <InputWrapper>
            <Label htmlFor="email">Email *</Label>
            <Input
              color={"black"}
              id="email"
              name="email"
              value={form.email}
              onChange={handleChange}
              required
              disabled={!!user}
              type="email"
            />
          </InputWrapper>
          <InputWrapper>
            <Label htmlFor="role">Rol</Label>
            <select
              id="role"
              name="role"
              value={form.role}
              onChange={handleChange}
              style={{
                width: '100%',
                padding: '12px 14px',
                borderRadius: '10px',
                border: '1.5px solid #e4e8f2',
                background: '#f9fbff',
                fontSize: '1rem',
                marginTop: '6px',
                color: 'black'
              }}
            >
              {roles.map(r => <option key={r} value={r}>{r}</option>)}
            </select>
          </InputWrapper>
          {user && (
            <InputWrapper style={{flexDirection: 'row', alignItems: 'center', marginTop: '8px'}}>
              <input
                type="checkbox"
                name="active"
                checked={form.active}
                onChange={handleChange}
                style={{marginRight: 8, color: "black"}}
              />
              <Label htmlFor="active" style={{marginBottom: 0}}>Activo</Label>
            </InputWrapper>
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