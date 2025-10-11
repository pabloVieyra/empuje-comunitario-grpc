import axios, { type AxiosRequestHeaders } from "axios";
import { authService } from "./authService";

const baseURL = "http://localhost:5169/";

export const apiClient = axios.create({
  baseURL,
});

apiClient.interceptors.request.use((config) => {
  const token = authService.getToken();
  if (token && config.url !== "/Auth/Login") {
    if (!config.headers) {
      config.headers = {} as AxiosRequestHeaders;
    }
    config.headers['Authorization'] = `${token}`;
  }
  return config;
});