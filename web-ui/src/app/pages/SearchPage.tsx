import { useState } from 'react'
import { Navigate, useSearchParams } from 'react-router'
import { useDebounce } from 'react-use'

import { SearchMovies } from '@/features/movies/components/SearchMovies'
import { SearchPeople } from '@/features/people/components/SearchPeople'
import { SearchHistory } from '@/features/search/components/SearchHistory'

import { useUser } from '@/hooks/userContext'

export const SearchPage = () => {
  const [reloadFlag, setReloadFlag] = useState<boolean>(false)
  const [debounced, setDebounced] = useState<string | null>(null)
  const [searchParams, setSearchParams] = useSearchParams()
  const query = searchParams.get('query')

  useDebounce(
    () => {
      setDebounced(query)
    },
    1000,
    [query],
  )
  const [user] = useUser()

  if (!user?.token) {
    return <Navigate to='/login' replace />
  }

  const updateQuery = (newQuery: string) => {
    setSearchParams({ query: newQuery })
  }

  return (
    <div className='space-y-4'>
      <h2>Search for movies and people</h2>

      <SearchHistory
        token={user.token}
        query={query}
        reload={reloadFlag}
        onQueryChange={updateQuery}
      />

      {debounced && (
        <>
          <SearchMovies
            query={debounced}
            token={user.token}
            reload={() => setReloadFlag((r) => !r)}
          />

          <div className='w-full h-px bg-neutral-200' />

          <SearchPeople
            query={debounced}
            token={user.token}
            reload={() => setReloadFlag((r) => !r)}
          />
        </>
      )}
    </div>
  )
}
