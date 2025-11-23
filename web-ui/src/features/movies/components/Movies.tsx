import Pagination from '@mui/material/Pagination'
import { useEffect, useState } from 'react'

import { Loader } from '@/components/Loader'

import { fetchMovies } from '../api'
import type { AllMovies } from '../types'
import { Movie } from './Movie'

export const Movies = () => {
  const [page, setPage] = useState(1)
  const handleChange = (_: React.ChangeEvent<unknown>, value: number) => {
    setPage(value)
  }
  const [movies, setMovies] = useState<AllMovies>()

  useEffect(() => {
    const load = async () => {
      setMovies(undefined) // Show loader on new page

      const data = await fetchMovies(page)
      setMovies(data)
    }

    load()
  }, [page])

  return (
    <div className='space-y-4'>
      <div role='list' className='flex flex-col gap-6'>
        <Loader data={movies} type='vertical'>
          {(loaded) =>
            loaded.data?.map((movie, index) => (
              <Movie
                key={movie.id}
                movie={movie}
                titleCount={(loaded.page - 1) * loaded.pageSize + index + 1}
              />
            ))
          }
        </Loader>
      </div>

      {movies && (
        <Pagination
          className='flex justify-center'
          count={movies.totalPages}
          page={page}
          onChange={handleChange}
        />
      )}
    </div>
  )
}
