import { useState, useEffect } from "react";
import { getUsers, createUser, updateUser, deactivateUser } from "@/services/userService";
import type { User } from "@/domain/User";

export const useUsers = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchUsers = async () => {
    setLoading(true);
    try {
      setUsers(await getUsers());
    } catch (e: any) {
      setError(e.message || "Error cargando usuarios");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { fetchUsers(); }, []);

  const addUser = async (userData: Omit<User, "id" | "activo">) => {
    setLoading(true);
    try {
      const newUser = await createUser(userData);
      setUsers([...users, newUser]);
    } catch (e: any) {
      setError(e.message || "Error creando usuario");
    } finally {
      setLoading(false);
    }
  };

  const editUser = async (id: string, data: Partial<Omit<User, "id" | "clave">>) => {
    setLoading(true);
    try {
      const updated = await updateUser(id, data);
      setUsers(users.map(u => u.id === id ? updated : u));
    } catch (e: any) {
      setError(e.message || "Error modificando usuario");
    } finally {
      setLoading(false);
    }
  };

  const deactivate = async (id: string) => {
    setLoading(true);
    try {
      const updated = await deactivateUser(id);
      setUsers(users.map(u => u.id === id ? updated : u));
    } catch (e: any) {
      setError(e.message || "Error inactivando usuario");
    } finally {
      setLoading(false);
    }
  };

  return { users, loading, error, addUser, editUser, deactivate, fetchUsers, setError };
};