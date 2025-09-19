const TOKEN_KEY = "access_token";
const ROLE_KEY = "role";

export const authService = {
  setToken(token: string) {
    localStorage.setItem(TOKEN_KEY, token);
  },
  getToken(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  },
  clearToken() {
    localStorage.removeItem(TOKEN_KEY);
  },
  setRole(role: string) {
    localStorage.setItem(ROLE_KEY, role);
  },
  getRole(): string | null {
    return localStorage.getItem(ROLE_KEY);
  },
  clearRole() {
    localStorage.removeItem(ROLE_KEY);
  },
};