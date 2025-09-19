const TOKEN_KEY = "access_token";

export const authService = {
  setToken(token: string) {
    console.log(token)
    localStorage.setItem(TOKEN_KEY, token);
  },
  getToken(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  },
  clearToken() {
    localStorage.removeItem(TOKEN_KEY);
  },
};