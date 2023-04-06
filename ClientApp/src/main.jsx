import React from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { ConfigProvider } from "antd";
import App from "./App";
import Login from "./pages/Login";
import BookTickets from "./pages/BookTickets";
import "./index.css";
import Forms from "./pages/Forms";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Login />,
  },
  {
    path: "/home",
    element: <App />,
    children: [
      {
        path: "/home/book",
        element: <BookTickets />,
      },
      {
        path: "/home/forms",
        element: <Forms />,
      },
    ],
  },
]);

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <ConfigProvider
      theme={{
        token: {
          colorPrimary: "#01346b",
        },
        components: {
          Menu: {
            colorTextBase: "#fff",
          },
        },
      }}
    >
      <RouterProvider router={router} />
    </ConfigProvider>
  </React.StrictMode>
);
