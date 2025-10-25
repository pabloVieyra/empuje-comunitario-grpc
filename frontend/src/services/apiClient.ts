import axios, { type AxiosRequestHeaders } from "axios";
import { authService } from "./authService";
import { use } from "react";

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
    config.headers['userId'] = "dc7a68d3-1072-42b1-8f74-ef062b2257e6"; // Si tu API requiere este header
  }
  return config;
});