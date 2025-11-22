import type { CastType } from "./types";

export const fetchCast = async (movieId: string): Promise<CastType> => {
  const response = await fetch(`/api/cast/${movieId}`);
  const data = await response.json();
  return data;
};
