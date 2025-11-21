import { useEffect, useState } from "react";
import { fetchMovies } from "../api";
import type { AllMovies } from "../types";
import { Loader } from "../../../shared/components/Loader";
import Pagination from "@mui/material/Pagination";
import { Movie } from "./Movie";
import { InputForm } from "../../../components/InputForm";

type Props = {
  search: string | undefined;
  updateSearch: (newSearch: string) => void;
};

export const Movies = ({ search, updateSearch }: Props) => {
  const [page, setPage] = useState(1);
  const handleChange = (event: React.ChangeEvent<unknown>, value: number) => {
    setPage(value);
  };
  const [movies, setMovies] = useState<AllMovies>();

  useEffect(() => {
    const load = async () => {
      const data = await fetchMovies(page);
      setMovies(data);
    };

    load();
  }, [page, search]);

  return (
    <div>
      <h2>Movies</h2>

      <InputForm
        id="search-page"
        label="Search for Movies:"
        type="text"
        value={search}
        onChange={(e) => updateSearch(e.target.value)}
      />

      <div role="list" className="movies">
        <Loader data={movies}>
          {(loaded) =>
            loaded.data?.map((movie, index) => (
              <Movie
                key={movie.id}
                movie={movie}
                titleCount={(loaded.page - 1) * loaded.pageSize + index + 1}
              />
            ))
          }
        </Loader>
      </div>

      {movies && (
        <Pagination
          className="movies-pagination"
          count={movies.totalPages}
          page={page}
          onChange={handleChange}
        />
      )}
    </div>
  );
};
