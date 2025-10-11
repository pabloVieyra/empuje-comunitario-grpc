import type { Event } from "@/domain/Event";
import { apiClient } from "./apiClient";


// GET: Lista todos los eventos
export async function getEvents(): Promise<Event[]> {
  try {
    const { data } = await apiClient.get("/Event/ListEventsAsync");
    return data?.data.events || [];
  } catch {
    return [];
  }
}

// GET: Evento por ID
export async function getEventById(eventId: number): Promise<Event | null> {
  try {
    const { data } = await apiClient.get("/Event/FindEventByIdAsync", { params: { eventId } });
    if (!data || (typeof data === "string" && data.startsWith("<!doctype html"))) {
      return null;
    }
    return data;
  } catch {
    return null;
  }
}

// POST: Crear evento
export async function createEvent(event: Omit<Event, "eventId">): Promise<Event> {
  const { data } = await apiClient.post("/Event/CreateEventAsync", event);
  return data;
}

// PUT: Actualizar evento
export async function updateEvent(event: Event): Promise<Event> {
  const { data } = await apiClient.put("/Event/UpdateEventAsync", event);
  return data;
}

// DELETE: Eliminar evento
export async function deleteEvent(eventId: number, actorId: string): Promise<void> {
  await apiClient.delete("/Event/DeleteEventAsync", { data: { eventId, actorId } });
}

// POST: Agregar usuario a evento
export async function addUserToEvent(eventId: number, userId: string, actorId: string) {
  return apiClient.post("/Event/AddUserToEventAsync", { eventId, userId, actorId });
}

// POST: Quitar usuario de evento
export async function removeUserFromEvent(eventId: number, userId: string, actorId: string) {
  return apiClient.post("/Event/RemoveUserFromEventAsync", { eventId, userId, actorId });
}

// POST: Registrar donación a evento
export async function registerDonationToEvent(eventId: number, donationId: string, quantity: number, actorId: string) {
  return apiClient.post("/Event/RegisterDonationToEventAsync", { eventId, donationId, quantity, actorId });
}