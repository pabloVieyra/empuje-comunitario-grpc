import { Outlet, Navigate } from "react-router";
import { useAuth } from "../hooks/useAuth";

// Layouts protegidos
export const PublicLayout = () => <Outlet />;
export const ProtectedLayout = () => {
  const { isAuthenticated } = useAuth();
  return isAuthenticated ? <Outlet /> : <Navigate to="/login" />;
};
export const PresidenteLayout = () => {
  const { role } = useAuth();
  return role === "PRESIDENTE" ? <Outlet /> : <Navigate to="/" />;
};