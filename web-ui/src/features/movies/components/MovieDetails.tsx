import { useEffect, useState } from 'react'

import {
  HorizontalContent,
  HorizontalContentItem,
} from '@/components/HorizontalContent'
import { Image } from '@/components/Image'
import { Loader } from '@/components/Loader'

import { fetchMovie, fetchRelatedMovies } from '../api'
import type { MovieDetails as MovieDetailsType, RelatedMovies } from '../types'
import { MovieRatings } from './MovieRatings'
import { Runtime } from './Runtime'

type Props = {
  id: string
  cast: React.ReactElement
  rate: React.ReactElement
  bookmark: React.ReactElement
}

export const MovieDetails = ({ id, cast, rate, bookmark }: Props) => {
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
          <div className='space-y-2'>
            <Image
              src={loaded.poster || 'https://placehold.co/300x444'}
              alt={loaded.title}
              className='w-auto h-auto'
            />

            <div>
              <h2 className='flex gap-2'>
                {loaded.title} {bookmark}
              </h2>

              <div className='space-y-2'>
                <div className='flex gap-2'>
                  <span>{loaded.year}</span>
                  <Runtime minutes={loaded.runTimeInMinutes} />
                  <span>{loaded.isAdult ? '18+' : 'All ages'}</span>
                </div>

                <p className='movie-plot'>{loaded.plot}</p>

                <div>Genres: {loaded.genres?.join(', ')}</div>

                <MovieRatings
                  averageRating={loaded.averageRating}
                  numberOfVotes={loaded.numberOfVotes}
                />

                {rate}
              </div>
            </div>
          </div>
        )}
      </Loader>

      {cast}

      <Loader data={relatedMovies} type='horizontal'>
        {(loaded) => (
          <div>
            <h3>Related movies</h3>

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
          </div>
        )}
      </Loader>
    </div>
  )
}
