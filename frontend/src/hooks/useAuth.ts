import { useState, useEffect } from "react";
import { authService } from "@/services/authService";

export const useAuth = () => {
  const [isAuthenticated, setIsAuthenticated] = useState(!!authService.getToken());

  useEffect(() => {
    const handleStorage = () => {
      setIsAuthenticated(!!authService.getToken());
    };

    window.addEventListener("storage", handleStorage);
    return () => window.removeEventListener("storage", handleStorage);
  }, []);

  // También actualiza el estado cuando el componente se renderiza
  useEffect(() => {
    setIsAuthenticated(!!authService.getToken());
  }, []);

  const role = "PRESIDENTE"; // Puedes obtener el rol del token si lo incluyes

  return {
    isAuthenticated,
    role,
    logout: () => {
      authService.clearToken();
      setIsAuthenticated(false);
    },
  };
};