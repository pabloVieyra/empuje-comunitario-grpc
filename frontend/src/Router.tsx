import { createBrowserRouter } from "react-router";

import { PublicLayout, ProtectedLayout, PresidenteLayout } from "./layouts/Layouts";
import Home from "./pages/Home";
import UserList from "./pages/users/UserList";
import Login from "./pages/login/Login";
import NotFound from "./pages/NotFound";


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