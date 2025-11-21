import type { AllPeopleType } from "./types";

export const fetchPeople = async (page: number = 1): Promise<AllPeopleType> => {
  const response = await fetch(`/api/people?page=${page}`);
  const data = await response.json();
  return data;
};
