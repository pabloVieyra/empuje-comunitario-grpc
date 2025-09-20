import type { User } from "@/domain/User";
import { apiClient } from "./apiClient";

export async function getUsers(): Promise<User[]> {
  const { data } = await apiClient.get("/Usuarios/ListUsersAsync");
  console.log(data)
  return data.data.users || [];
}

export async function createUser(user: Omit<User, "id" | "activo">): Promise<User> {
  const { data } = await apiClient.post("/Usuarios/CreateUserAsync", user);
  return data;
}

export async function updateUser(id: string, user: Partial<Omit<User, "id" | "clave">>): Promise<User> {
  const { data } = await apiClient.put(`/Usuarios/UpdateUserAsync`, { id, ...user });
  return data;
}

export async function deactivateUser(id: string): Promise<User> {
  const { data } = await apiClient.patch(`/Usuarios/DisableUserAsync/${id}`);
  return data;
}