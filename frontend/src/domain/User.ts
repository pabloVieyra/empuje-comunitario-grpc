export type UserRole = "ADMIN" | "PRESIDENT" | "USER";

export interface User {
  id: string;
  username: string;
  name: string;
  lastName: string;
  phone?: string;
  email: string;
  role: UserRole;
  active: boolean;
}