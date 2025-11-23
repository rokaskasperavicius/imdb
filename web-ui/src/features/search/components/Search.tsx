import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router'

import { Loader } from '@/components/Loader'

import { fetchSearches } from '../api'
import type { AllSearches } from '../types'
import { SearchHistoryItem } from './SearchHistoryItem'

type Props = {
  token: string
}

export const Search = ({ token }: Props) => {
  const navigate = useNavigate()
  const [searches, setSearches] = useState<AllSearches>()

  useEffect(() => {
    const load = async () => {
      const data = await fetchSearches(token, navigate)
      setSearches(data)
    }

    load()
  }, [token, navigate])

  return (
    <div>
      <Loader data={searches} type='vertical'>
        {(loaded) =>
          loaded
            .sort(
              (a, b) =>
                new Date(b.createdAt).getTime() -
                new Date(a.createdAt).getTime(),
            )
            .map((search) => (
              <SearchHistoryItem key={search.id} item={search} />
            ))
        }
      </Loader>
    </div>
  )
}
