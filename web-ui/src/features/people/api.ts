import type { NavigateFunction } from 'react-router'

import type {
  AllPeopleBySearch,
  AllPeopleType,
  PersonDetails,
  RelatedPeople,
} from './types'

export const fetchPeople = async (page: number = 1): Promise<AllPeopleType> => {
  const response = await fetch(`/api/people?page=${page}&pageSize=10`)
  const data = await response.json()
  return data
}

// Authenticated
export const fetchPeopleBySearch = async (
  query: string,
  token: string,
  navigate: NavigateFunction,
): Promise<AllPeopleBySearch> => {
  const response = await fetch(
    `/api/people/search?query=${encodeURIComponent(query)}`,
    {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    },
  )

  if (response.status === 401) {
    navigate('/logout', { replace: true })

    return []
  }

  const data = await response.json()
  return data
}

export const fetchPerson = async (
  id: string,
  navigate: NavigateFunction,
): Promise<PersonDetails> => {
  const response = await fetch(`/api/people/${id}`)

  if (response.status === 404) {
    navigate('/movies', { replace: true })
  }

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
