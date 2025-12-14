import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router'

import { Loader } from '@/components/Loader'

import { fetchPeopleBySearch } from '../api'
import type { AllPeopleBySearch } from '../types'
import { Person } from './Person'

type Props = {
  query: string
  token: string
  reload: () => void
}

export const SearchPeople = ({ query, token, reload }: Props) => {
  const navigate = useNavigate()
  const [people, setPeople] = useState<AllPeopleBySearch>()

  useEffect(() => {
    const load = async () => {
      setPeople(undefined) // Show loader on new search

      const data = await fetchPeopleBySearch(query, token, navigate)
      setPeople(data)

      // Notifies parent to reload search history
      reload()
    }

    load()
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [query, token, navigate])

  return (
    <div>
      <h3>People</h3>

      <div role='list' className='flex flex-col gap-6'>
        <Loader data={people} type='vertical'>
          {(loaded) =>
            loaded.length === 0 ? (
              <p>No results found for "{query}"</p>
            ) : (
              loaded.map((person, index) => (
                <Person
                  key={person.id}
                  person={person}
                  personCount={index + 1}
                />
              ))
            )
          }
        </Loader>
      </div>
    </div>
  )
}
