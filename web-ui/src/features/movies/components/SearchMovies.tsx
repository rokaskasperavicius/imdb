import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router'

import { Loader } from '@/components/Loader'

import { fetchMoviesBySearch } from '../api'
import type { AllMoviesBySearch } from '../types'
import { Movie } from './Movie'

type Props = {
  query: string
  token: string
  reload: () => void
}

export const SearchMovies = ({ query, token, reload }: Props) => {
  const navigate = useNavigate()
  const [movies, setMovies] = useState<AllMoviesBySearch>()

  useEffect(() => {
    const load = async () => {
      const data = await fetchMoviesBySearch(query, token, navigate)

      setMovies(data)
      reload()
    }

    load()
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [query, token, navigate])

  return (
    <div role='list' className='flex flex-col gap-6'>
      <Loader data={movies} type='vertical'>
        {(loaded) =>
          loaded.map((movie, index) => (
            <Movie key={movie.id} movie={movie} titleCount={index + 1} />
          ))
        }
      </Loader>
    </div>
  )
}
