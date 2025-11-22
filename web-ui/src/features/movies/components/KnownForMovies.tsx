import { useEffect, useState } from "react";
import type { AllMoviesBatch } from "../types";
import { fetchMoviesBatch } from "../api";
import { Loader } from "../../../components/Loader";
import { Link } from "react-router";
import { Image } from "../../../components/Image";

type Props = {
  movieIds: string[];
};

export const KnownForMovies = ({ movieIds }: Props) => {
  const [movies, setMovies] = useState<AllMoviesBatch>();

  useEffect(() => {
    const load = async () => {
      const movieData = await fetchMoviesBatch(movieIds);

      setMovies(movieData);
    };

    load();
  }, [movieIds]);

  return (
    <Loader data={movies} type="horizontal">
      {(loaded) => (
        <div>
          <h3>Known For Movies</h3>

          <div className="flex gap-4 overflow-x-auto pb-2">
            {loaded.map((movie) => (
              <Link
                key={movie.id}
                className="w-32 flex-0 shrink-0 basis-auto"
                to={`/movies/${movie.id}`}
              >
                <Image
                  src={movie.poster || "https://placehold.co/100x150"}
                  alt={movie.title}
                  className="w-full h-[180px]"
                />
                <div>{movie.title}</div>
              </Link>
            ))}
          </div>
        </div>
      )}
    </Loader>
  );
};
