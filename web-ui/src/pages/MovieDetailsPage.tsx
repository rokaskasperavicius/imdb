import { useParams } from "react-router";
import { MovieDetails } from "../features/movies/components/MovieDetails";

export const MovieDetailsPage = () => {
  const params = useParams();
  const movieId = params.movieId;

  if (!movieId) {
    return <div>Movie ID is missing</div>;
  }

  return <MovieDetails id={movieId} />;
};
