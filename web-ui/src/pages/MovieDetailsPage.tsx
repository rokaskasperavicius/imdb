import { useParams } from "react-router";
import { MovieDetails } from "../features/movies/components/MovieDetails";
import { Cast } from "../features/cast/components/Cast";

export const MovieDetailsPage = () => {
  const params = useParams();
  const movieId = params.movieId;

  if (!movieId) {
    return null;
  }

  return <MovieDetails id={movieId} cast={<Cast movieId={movieId} />} />;
};
