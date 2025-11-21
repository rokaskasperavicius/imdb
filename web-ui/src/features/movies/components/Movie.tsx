import { Link } from "react-router";
import type { MovieType } from "../types";
import { Runtime } from "./Runtime";

type Props = {
  movie: MovieType;
  titleCount?: number;
};

export const Movie = ({ movie, titleCount }: Props) => {
  return (
    <Link to={`/movies/${movie.id}`} className="movie" role="listitem">
      <img
        src={movie.poster || "https://placehold.co/300x444"}
        alt={movie.title}
      />

      <div className="movie-content">
        <h4>
          {titleCount}. {movie.title}
        </h4>

        <div className="movie-content-first">
          <span>{movie.year}</span>
          <Runtime minutes={movie.runTimeInMinutes} />
          <span>{movie.isAdult ? "18+" : "All ages"}</span>
        </div>

        <p className="movie-plot">{movie.plot}</p>
        <>Genres: {movie.genres?.join(", ")}</>

        <div>
          {movie.averageRating} ({movie.numberOfVotes})
        </div>
      </div>
    </Link>
  );
};
