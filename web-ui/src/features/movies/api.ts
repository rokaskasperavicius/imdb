import type {
  AllMovies,
  AllMoviesBatch,
  MovieDetails,
  RelatedMovies,
} from "./types";

export const fetchMovies = async (page: number = 1): Promise<AllMovies> => {
  const response = await fetch(`/api/movies?page=${page}`);
  const data = await response.json();
  return data;
};

export const fetchMoviesBatch = async (
  ids: string[]
): Promise<AllMoviesBatch> => {
  const response = await fetch(
    `/api/movies/batch?${ids.map((id) => `ids=${id}`).join("&")}`
  );
  const data = await response.json();
  return data;
};

export const fetchMovie = async (id: string): Promise<MovieDetails> => {
  const response = await fetch(`/api/movies/${id}`);
  const data = await response.json();
  return data;
};

export const fetchRelatedMovies = async (
  id: string
): Promise<RelatedMovies> => {
  const response = await fetch(`/api/movies/${id}/related`);
  const data = await response.json();
  return data;
};
