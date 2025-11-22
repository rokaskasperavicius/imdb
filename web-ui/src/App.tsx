import { Routes, Route, Navigate } from "react-router";
import { Login } from "./pages/Login";
import { Register } from "./pages/Register";
import { Home } from "./pages/Home";
import { MainLayout } from "./layouts/MainLayout";
import { MoviesPage } from "./pages/Movies";
import { MovieDetailsPage } from "./pages/MovieDetailsPage";
import { PeoplePage } from "./pages/PeoplePage";
import { SearchPage } from "./pages/SearchPage";
import { PersonDetailsPage } from "./pages/PersonDetailsPage";
import { LogoutPage } from "./pages/Logout";
import "./App.css";

export const App = () => {
  return (
    <Routes>
      <Route element={<MainLayout />}>
        <Route index element={<Home />} />
        <Route path="movies" element={<MoviesPage />} />
        <Route path="movies/:movieId" element={<MovieDetailsPage />} />
        <Route path="people" element={<PeoplePage />} />
        <Route path="people/:personId" element={<PersonDetailsPage />} />
        <Route path="search" element={<SearchPage />} />
        <Route path="login" element={<Login />} />
        <Route path="register" element={<Register />} />
        <Route path="logout" element={<LogoutPage />} />
        <Route path="*" element={<Navigate to="/" replace />} />
      </Route>
    </Routes>
  );
};
