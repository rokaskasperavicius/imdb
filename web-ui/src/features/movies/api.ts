import type { AllMovies, MovieDetails, RelatedMovies } from "./types";

export const fetchMovies = async (page: number = 1): Promise<AllMovies> => {
  const response = await fetch(`/api/movies?page=${page}`);
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
