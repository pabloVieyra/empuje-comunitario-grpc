import { useState, useEffect } from "react";
import { getEvents, createEvent, updateEvent, deleteEvent } from "@/services/eventService";
import type { Event } from "@/domain/Event";

export const useEvents = () => {
  const [events, setEvents] = useState<Event[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchEvents = async () => {
    setLoading(true);
    try {
      setEvents(await getEvents());
    } catch (e: any) {
      setError(e.message || "Error cargando eventos");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { fetchEvents(); }, []);

  const addEvent = async (eventData: Omit<Event, "eventId">) => {
    setLoading(true);
    try {
      const newEvent = await createEvent(eventData);
      setEvents([...events, newEvent]);
    } catch (e: any) {
      setError(e.message || "Error creando evento");
    } finally {
      setLoading(false);
    }
  };

  const editEvent = async (event: Event) => {
    setLoading(true);
    try {
      const updated = await updateEvent(event);
      setEvents(events.map(e => e.eventId === event.eventId ? updated : e));
    } catch (e: any) {
      setError(e.message || "Error modificando evento");
    } finally {
      setLoading(false);
    }
  };

  const removeEvent = async (eventId: number, actorId: string) => {
    setLoading(true);
    try {
      await deleteEvent(eventId, actorId);
      setEvents(events.filter(e => e.eventId !== eventId));
    } catch (e: any) {
      setError(e.message || "Error eliminando evento");
    } finally {
      setLoading(false);
    }
  };

  return { events, loading, error, addEvent, editEvent, removeEvent, fetchEvents, setError };
}; 