import type { AllPeopleType, PersonDetails, RelatedPeople } from './types'

export const fetchPeople = async (page: number = 1): Promise<AllPeopleType> => {
  const response = await fetch(`/api/people?page=${page}`)
  const data = await response.json()
  return data
}

export const fetchPerson = async (id: string): Promise<PersonDetails> => {
  const response = await fetch(`/api/people/${id}`)
  const data = await response.json()
  return data
}

export const fetchRelatedPeople = async (
  id: string,
): Promise<RelatedPeople> => {
  const response = await fetch(`/api/people/${id}/related`)
  const data = await response.json()
  return data
}
