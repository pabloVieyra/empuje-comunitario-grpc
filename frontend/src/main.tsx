import './index.css'

import ReactDOM, { type Container } from "react-dom/client";
import { RouterProvider } from "react-router/dom";
import { router } from './Router';
import { Provider } from "@/components/ui/provider"



const root = document.getElementById("root") as Container;

ReactDOM.createRoot(root).render(
  <Provider>
    <RouterProvider router={router} />
  </Provider>
);
