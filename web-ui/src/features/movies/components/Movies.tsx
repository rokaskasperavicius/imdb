import { useEffect, useState } from 'react'

import { isArrayNotEmpty } from '@/common/helper'

import { Loader } from '@/components/Loader'
import { Pagination } from '@/components/Pagination/Pagination'

import { fetchMovies } from '../api'
import type { AllMovies } from '../types'
import { Movie } from './Movie'

type Props = {
  page: number
  onPageChange: (page: number) => void
}

export const Movies = ({ page, onPageChange }: Props) => {
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
    <div className='space-y-6'>
      <div role='list' className='flex flex-col gap-6'>
        <Loader data={movies} type='vertical'>
          {(loaded) =>
            isArrayNotEmpty(loaded.data) ? (
              loaded.data.map((movie, index) => (
                <Movie
                  key={movie.id}
                  movie={movie}
                  titleCount={(loaded.page - 1) * loaded.pageSize + index + 1}
                />
              ))
            ) : (
              <div>No movies found.</div>
            )
          }
        </Loader>
      </div>

      {movies && isArrayNotEmpty(movies.data) && (
        <Pagination
          page={page}
          count={movies.totalPages}
          onChange={onPageChange}
        />
      )}
    </div>
  )
}
