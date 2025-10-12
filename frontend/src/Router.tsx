import { createBrowserRouter } from "react-router";

import { PublicLayout, ProtectedLayout, PresidenteLayout } from "./layouts/Layouts";
import Home from "./pages/Home";
import Login from "./pages/login/Login";
import NotFound from "./pages/NotFound";
import { UserList } from "./pages/users/UserList";
import { DonationList } from "./pages/donation/DonationList";
import { EventList } from "./pages/event/EventList";
import EventDashboard from "./pages/event/dashboard/EventDashboard";
import { DonationDashboard } from "./pages/donation/dashboard/DonationDashboard";


export const router = createBrowserRouter([
  // Protected routes
  {
    path: "/",
    element: <ProtectedLayout />,
    children: [
      { index: true, element: <Home /> },

      {
        path: "usuarios",
        element: <PresidenteLayout />,
        children: [
          { index: true, element: <UserList /> },
        ],
      },
      {
        path: "donaciones",
        element: <PresidenteLayout />,
        children: [
          { index: true, element: <DonationList /> },
          { path: "dashboard", element: <DonationDashboard /> },
        ],
      },
      {
        path: "eventos",
        element: <PresidenteLayout />,
        children: [
          { index: true, element: <EventList /> },
          { path: "dashboard", element: <EventDashboard /> },
        ],
      }
    ],
  },

  // Public routes
  {
    path: "/",
    element: <PublicLayout />,
    children: [
      { path: "login", element: <Login /> },
    ],
  },

  // 404
  { path: "*", element: <NotFound /> },
]);