import { useState } from "react";
import { loginRequest } from "@/services/authApi";
import { authService } from "@/services/authService";

type LoginValues = { usernameOrEmail: string; password: string };

export const useLogin = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const login = async (values: LoginValues) => {
    setLoading(true);
    setError(null);

    try {
      const {data} = await loginRequest(values.usernameOrEmail, values.password);
      const token = data.token;
      if (token) {
        authService.setToken(token);
        return true;
      } else {
        setError("Token inválido.");
        return false;
      }
    } catch (err: any) {
      setError(
        err?.response?.data?.message ||
        "Usuario/email o contraseña incorrectos."
      );
      return false;
    } finally {
      setLoading(false);
    }
  };

  return {
    loading,
    error,
    login,
    setError,
  };
};