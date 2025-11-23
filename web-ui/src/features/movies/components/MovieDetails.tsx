import { useEffect, useState } from 'react'
import { Link } from 'react-router'

import { Image } from '@/components/Image'
import { Loader } from '@/components/Loader'

import { fetchMovie, fetchRelatedMovies } from '../api'
import type { MovieDetails as MovieDetailsType, RelatedMovies } from '../types'
import { Runtime } from './Runtime'

type Props = {
  id: string
  cast: React.ReactElement
}

export const MovieDetails = ({ id, cast }: Props) => {
  const [movie, setMovie] = useState<MovieDetailsType>()
  const [relatedMovies, setRelatedMovies] = useState<RelatedMovies>()

  useEffect(() => {
    const load = async () => {
      const movieData = await fetchMovie(id)
      const relatedMoviesData = await fetchRelatedMovies(id)

      setMovie(movieData)
      setRelatedMovies(relatedMoviesData)
    }

    load()
  }, [id])

  return (
    <div className='space-y-4'>
      <Loader data={movie} type='vertical'>
        {(loaded) => (
          <div>
            <Image
              src={loaded.poster || 'https://placehold.co/300x444'}
              alt={loaded.title}
              className='w-auto'
            />

            <div className='movie-content'>
              <h2>{loaded.title}</h2>

              <div className='movie-content-first'>
                <span>{loaded.year}</span>
                <Runtime minutes={loaded.runTimeInMinutes} />
                <span>{loaded.isAdult ? '18+' : 'All ages'}</span>
              </div>

              <p className='movie-plot'>{loaded.plot}</p>
              <>Genres: {loaded.genres?.join(', ')}</>

              <div>
                {loaded.averageRating} ({loaded.numberOfVotes})
              </div>
            </div>
          </div>
        )}
      </Loader>

      {cast}

      <Loader data={relatedMovies} type='horizontal'>
        {(loaded) => (
          <div>
            <h3>Related Movies</h3>

            <div className='flex gap-4 overflow-x-auto pb-2'>
              {loaded.map((movie) => (
                <Link
                  key={movie.id}
                  className='w-32 flex-0 shrink-0 basis-auto'
                  to={`/movies/${movie.id}`}
                >
                  <Image
                    src={movie.poster || 'https://placehold.co/100x150'}
                    alt={movie.title}
                    className='w-full h-[180px]'
                  />
                  <div>{movie.title}</div>
                </Link>
              ))}
            </div>
          </div>
        )}
      </Loader>
    </div>
  )
}
