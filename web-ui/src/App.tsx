import { Routes, Route } from "react-router";
import { Login } from "./pages/Login";
import { Register } from "./pages/Register";
import { Home } from "./pages/Home";
import { MainLayout } from "./shared/components/MainLayout";
import { MoviesPage } from "./pages/Movies";
import { MovieDetailsPage } from "./pages/MovieDetailsPage";
import { PeoplePage } from "./pages/PeoplePage";
import "./App.css";
import "./features/movies/movies.css";
import "./features/people/people.css";

export const App = () => {
  return (
    <Routes>
      <Route element={<MainLayout />}>
        <Route index element={<Home />} />
        <Route path="movies" element={<MoviesPage />} />
        <Route path="movies/:movieId" element={<MovieDetailsPage />} />
        <Route path="people" element={<PeoplePage />} />
        <Route path="login" element={<Login />} />
        <Route path="register" element={<Register />} />
      </Route>
    </Routes>
  );
};
