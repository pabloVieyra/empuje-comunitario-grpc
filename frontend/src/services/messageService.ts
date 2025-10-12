import { apiClient } from "./apiClient";

// POST: Solicitar donaciones
export async function requestDonation(payload: {
  requesterOrgId: string,
  requestId: string,
  donations: { category: string, description: string, quantity: number }[]
}) {
  const { data } = await apiClient.post("/Messages/RequestDonation", payload);
  return data;
}

// POST: Transferir donaciones
export async function transferDonation(
  idOrganizacionSolicitante: string,
  payload: {
    requestId: string,
    donationOrgId: string,
    donations: { category: string, description: string, quantity: number }[]
  }
) {
  const { data } = await apiClient.post(
    `/Messages/TransfersDonation?idOrganizacionSolicitante=${idOrganizacionSolicitante}`,
    payload
  );
  return data;
}

// POST: Ofrecer donaciones
export async function offerDonations(payload: {
  offerId: string,
  donationOrganizationId: string,
  donations: { category: string, description: string, quantity: number }[]
}) {
  const { data } = await apiClient.post("/Messages/OffersDonations", payload);
  return data;
}

// POST: Baja de solicitud
export async function cancelRequestDonation(payload: {
  requestOrgId: string,
  requestId: string
}) {
  const { data } = await apiClient.post("/Messages/RequestsCancel", payload);
  return data;
}

// POST: Publicar evento solidario
export async function createSolidaryEvent(payload: {
  orgId: string,
  eventId: string,
  nameEvent: string,
  description: string,
  dateTimeEvent: string
}) {
  const { data } = await apiClient.post("/Messages/EventsSolidary", payload);
  return data;
}

// POST: Baja de evento solidario
export async function cancelSolidaryEvent(payload: {
  orgId: string,
  eventId: string
}) {
  const { data } = await apiClient.post("/Messages/EventsCancel", payload);
  return data;
}

// POST: Notificar adhesión voluntario a evento
export async function notifyEventVolunteer(
  idOrganizador: string,
  payload: {
    eventId: string,
    orgId: string,
    volunteerId: string,
    name: string,
    lastName: string,
    phone: string,
    email: string
  }
) {
  const { data } = await apiClient.post(
    `/Messages/EventsVolunteer?idOrganizador=${idOrganizador}`,
    payload
  );
  return data;
}