export interface Event {
  eventId: number;
  eventName: string;
  description: string;
  eventDateTime: string; // ISO string
  creationUserId: string;
}