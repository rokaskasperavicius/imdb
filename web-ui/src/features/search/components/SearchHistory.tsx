import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router'

import { InputForm } from '@/components/InputForm'
import { Loader } from '@/components/Loader'

import { fetchSearches } from '../api'
import type { AllSearches } from '../types'
import { SearchHistoryItem } from './SearchHistoryItem'

type Props = {
  token: string
  query: string | null
  reload: boolean
  onQueryChange: (newQuery: string) => void
}

export const SearchHistory = ({
  token,
  query,
  reload,
  onQueryChange,
}: Props) => {
  const navigate = useNavigate()
  const [searches, setSearches] = useState<AllSearches>()

  useEffect(() => {
    const load = async () => {
      const data = await fetchSearches(token, navigate)
      setSearches(data)
    }

    load()
  }, [token, reload, navigate])

  return (
    <div>
      <Loader data={searches} type='vertical-narrow'>
        {(loaded) =>
          loaded?.length === 0 ? null : (
            <div>
              <h3 className='mb-1'>Previous searches</h3>

              <div role='list' className='max-h-52 overflow-y-auto'>
                {loaded
                  .sort(
                    (a, b) =>
                      new Date(b.createdAt).getTime() -
                      new Date(a.createdAt).getTime(),
                  )
                  .map((search) => (
                    <SearchHistoryItem
                      key={search.id}
                      item={search}
                      onClick={() => onQueryChange(search.query)}
                    />
                  ))}
              </div>
            </div>
          )
        }
      </Loader>

      <InputForm
        label='Search'
        id='search'
        type='text'
        value={query || ''}
        onChange={(e) => onQueryChange(e.target.value)}
      />
    </div>
  )
}
