import { apiClient } from "./apiClient";

// GET: Todos los eventos externos
export async function getAllExternalEvents() {
  const { data } = await apiClient.get("/ExternalData/GetAllEvents");
  return data?.data ?? [];
}

// GET: Voluntarios por evento externo
export async function getAllVolunteersByEvent(eventId: string) {
  const { data } = await apiClient.get(`/ExternalData/GetAllVolunteerByEvent/${eventId}`);
  return data?.data ?? [];
}

// GET: Todas las ofertas de donación externas
export async function getAllOfferDonations() {
  const { data } = await apiClient.get("/ExternalData/GetAllOfferDonation");
  return data?.data ?? [];
}

// GET: Todas las solicitudes de donación externas
export async function getAllRequestDonations() {
  const { data } = await apiClient.get("/ExternalData/GetAllRequestsDonation");
  return data?.data ?? [];
}