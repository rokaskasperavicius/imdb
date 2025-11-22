import { Outlet } from "react-router";
import { Header } from "./Header";

export const MainLayout = () => {
  return (
    <div className="layout">
      <Header />

      <div className="content">
        <Outlet />
      </div>
    </div>
  );
};
