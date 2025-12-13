import type { NavigateFunction } from 'react-router'

import type { AllRatings } from './types'

export const fetchRatings = async (
  token: string,
  navigate: NavigateFunction,
): Promise<AllRatings> => {
  const response = await fetch(`/api/ratings`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  })

  if (response.status === 401) {
    navigate('/logout', { replace: true })

    return []
  }

  const data = await response.json()
  return data
}

export const rateMovie = async (
  movieId: string,
  rating: number,
  token: string,
  navigate: NavigateFunction,
) => {
  const response = await fetch(`/api/ratings/${movieId}/rate`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify({ rating }),
  })

  if (response.status === 401) {
    navigate('/logout', { replace: true })
  }
}
