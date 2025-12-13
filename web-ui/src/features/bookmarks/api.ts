import type { NavigateFunction } from 'react-router'

import type { AllBookmarks } from './types'

export const fetchBookmarks = async (
  token: string,
  navigate: NavigateFunction,
): Promise<AllBookmarks> => {
  const response = await fetch(`/api/bookmarks`, {
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

export const bookmarkMovie = async (
  movieId: string,
  token: string,
  navigate: NavigateFunction,
) => {
  const response = await fetch(`/api/bookmarks/${movieId}`, {
    method: 'POST',
    headers: {
      Authorization: `Bearer ${token}`,
    },
  })

  if (response.status === 401) {
    navigate('/logout', { replace: true })
  }
}

export const deleteBookmark = async (
  movieId: string,
  token: string,
  navigate: NavigateFunction,
) => {
  const response = await fetch(`/api/bookmarks/${movieId}`, {
    method: 'DELETE',
    headers: {
      Authorization: `Bearer ${token}`,
    },
  })

  if (response.status === 401) {
    navigate('/logout', { replace: true })
  }
}
