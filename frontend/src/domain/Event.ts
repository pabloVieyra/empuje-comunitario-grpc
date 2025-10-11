export interface Event {
  id: number;
  eventName: string;
  description: string;
  eventDateTime: string; // ISO string
  creationUserId?: string;
  modifiedUserId?: string;
  participants?: string[]; // Array de IDs de usuarios participantes
}