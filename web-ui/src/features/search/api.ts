import type { NavigateFunction } from 'react-router'

import type { AllSearches } from './types'

export const fetchSearches = async (
  token: string,
  navigate: NavigateFunction,
): Promise<AllSearches> => {
  const response = await fetch(`/api/search`, {
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
