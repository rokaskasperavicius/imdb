import { useEffect, useState } from "react";
import type { MovieDetails as MovieDetailsType, RelatedMovies } from "../types";
import { fetchMovie, fetchRelatedMovies } from "../api";
import { Loader } from "../../../shared/components/Loader";
import { Runtime } from "./Runtime";
import { Link } from "react-router";

type Props = {
  id: string;
};

export const MovieDetails = ({ id }: Props) => {
  const [movie, setMovie] = useState<MovieDetailsType>();
  const [relatedMovies, setRelatedMovies] = useState<RelatedMovies>();

  useEffect(() => {
    const load = async () => {
      const movieData = await fetchMovie(id);
      const relatedMoviesData = await fetchRelatedMovies(id);

      setMovie(movieData);
      setRelatedMovies(relatedMoviesData);
    };

    load();
  }, [id]);

  return (
    <div>
      <Loader data={movie}>
        {(loaded) => (
          <div>
            <img
              src={loaded.poster || "https://placehold.co/300x444"}
              alt={loaded.title}
            />

            <div className="movie-content">
              <h2>{loaded.title}</h2>

              <div className="movie-content-first">
                <span>{loaded.year}</span>
                <Runtime minutes={loaded.runTimeInMinutes} />
                <span>{loaded.isAdult ? "18+" : "All ages"}</span>
              </div>

              <p className="movie-plot">{loaded.plot}</p>
              <>Genres: {loaded.genres?.join(", ")}</>

              <div>
                {loaded.averageRating} ({loaded.numberOfVotes})
              </div>
            </div>
          </div>
        )}
      </Loader>

      <Loader data={relatedMovies}>
        {(loaded) => (
          <div>
            <h3>Related Movies</h3>

            <div className="related-movies">
              {loaded.map((movie) => (
                <Link
                  key={movie.id}
                  className="related-movie"
                  to={`/movies/${movie.id}`}
                >
                  <img
                    src={movie.poster || "https://placehold.co/100x150"}
                    alt={movie.title}
                  />
                  <div>{movie.title}</div>
                </Link>
              ))}
            </div>
          </div>
        )}
      </Loader>
    </div>
  );
};
