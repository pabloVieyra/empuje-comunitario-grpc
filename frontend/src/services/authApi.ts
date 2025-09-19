import { apiClient } from "./apiClient";

// Puedes cambiar la URL sin tocar ni el hook ni el componente
export async function loginRequest(usernameOrEmail: string, password: string) {
  const response = await apiClient.post("/auth/login", {
    usernameOrEmail,
    password,
  });
  // Aquí podría devolver el token, usuario, roles, etc
  return response.data;
}