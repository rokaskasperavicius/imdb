import { useSearchParams } from "react-router";
import { Movies } from "../features/movies/components/Movies";

export const MoviesPage = () => {
  const [searchParams, setSearchParams] = useSearchParams();
  const searchValue = searchParams.get("search") ?? "";

  return (
    <div>
      <Movies
        search={searchValue}
        updateSearch={(newSearch) => setSearchParams({ search: newSearch })}
      />
    </div>
  );
};
