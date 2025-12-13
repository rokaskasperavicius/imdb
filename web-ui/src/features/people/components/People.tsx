import { useEffect, useState } from 'react'

import { Loader } from '@/components/Loader'
import { Pagination } from '@/components/Pagination/Pagination'

import { fetchPeople } from '../api'
import type { AllPeopleType } from '../types'
import { Person } from './Person'

export const People = () => {
  const [page, setPage] = useState(1)
  const [people, setPeople] = useState<AllPeopleType>()

  useEffect(() => {
    const load = async () => {
      setPeople(undefined) // Show loader on new page

      const data = await fetchPeople(page)
      setPeople(data)
    }

    load()
  }, [page])

  return (
    <div className='space-y-6'>
      <div role='list' className='flex flex-col gap-6'>
        <Loader data={people} type='vertical'>
          {(loaded) =>
            loaded.data?.map((person, index) => (
              <Person
                key={person.id}
                person={person}
                personCount={(loaded.page - 1) * loaded.pageSize + index + 1}
              />
            ))
          }
        </Loader>
      </div>

      {people && (
        <Pagination count={people.totalPages} page={page} onChange={setPage} />
      )}
    </div>
  )
}
