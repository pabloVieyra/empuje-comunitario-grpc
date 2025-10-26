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
import DonationReport from "./pages/donation/report/DonationReport";
import EventParticipationReport from "./pages/event/report/EventParticipationReport";
import OrganizationConsult from "./pages/organization/OrganizationConsult";


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
          { path: "informe", element: <DonationReport /> },

        ],
      },
      {
        path: "eventos",
        element: <PresidenteLayout />,
        children: [
          { index: true, element: <EventList /> },
          { path: "dashboard", element: <EventDashboard /> },
          { path: "informe", element: <EventParticipationReport /> }

        ],
      },
      {
  path: "organization",
  element: <PresidenteLayout />,
  children: [
    { path: "consulta", element: <OrganizationConsult /> }
  ]
},
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