import axios, { type AxiosRequestHeaders } from "axios";
import { authService } from "./authService";

// Puedes setear la baseURL acá, por ejemplo para tu cliente externo en C#:
const baseURL = "https://tudominio-cliente-csharp.com/api";

export const apiClient = axios.create({
  baseURL,
});

// Si quieres que todas las requests (menos login) incluyan el token:
apiClient.interceptors.request.use((config) => {
  const token = authService.getToken();
  if (token && config.url !== "/auth/login") {
    if (!config.headers) {
      config.headers = {} as AxiosRequestHeaders;
    }
    config.headers['Authorization'] = `Bearer ${token}`;
  }
  return config;
});