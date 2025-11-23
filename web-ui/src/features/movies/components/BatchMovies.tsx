import { useEffect, useState } from 'react'

import {
  HorizontalContent,
  HorizontalContentItem,
} from '@/components/HorizontalContent'
import { Image } from '@/components/Image'
import { Loader } from '@/components/Loader'

import { fetchMoviesBatch } from '../api'
import type { AllMoviesBatch } from '../types'

type Props = {
  title: string
  movieIds: string[]
  emptyTitle: string
}

export const BatchMovies = ({ title, emptyTitle, movieIds }: Props) => {
  const [movies, setMovies] = useState<AllMoviesBatch>()

  useEffect(() => {
    const load = async () => {
      const movieData = await fetchMoviesBatch(movieIds)

      setMovies(movieData)
    }

    load()
  }, [movieIds])

  return (
    <Loader data={movies} type='horizontal'>
      {(loaded) => (
        <div>
          <h3>{title}</h3>

          {loaded.length > 0 ? (
            <HorizontalContent>
              {loaded.map((movie) => (
                <HorizontalContentItem
                  key={movie.id}
                  to={`/movies/${movie.id}`}
                >
                  <Image
                    src={movie.poster}
                    alt={movie.title || 'Movie Poster'}
                  />
                  <div>{movie.title}</div>
                </HorizontalContentItem>
              ))}
            </HorizontalContent>
          ) : (
            <div>{emptyTitle}</div>
          )}
        </div>
      )}
    </Loader>
  )
}
