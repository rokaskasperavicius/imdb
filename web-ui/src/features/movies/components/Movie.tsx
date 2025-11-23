import { Link } from 'react-router'

import { Image } from '@/components/Image'

import type { MovieType } from '../types'
import { MovieRatings } from './MovieRatings'
import { Runtime } from './Runtime'

type Props = {
  movie: MovieType
  titleCount?: number
}

export const Movie = ({ movie, titleCount }: Props) => {
  return (
    <Link
      to={`/movies/${movie.id}`}
      className='hover:bg-neutral-700 flex gap-4'
      role='listitem'
    >
      <div className='flex-0 shrink-0 basis-auto'>
        <Image
          src={movie.poster || 'https://placehold.co/300x444'}
          alt={movie.title}
        />
      </div>

      <div>
        <h4>
          {titleCount}. {movie.title}
        </h4>

        <div className='flex gap-2'>
          <span>{movie.year}</span>
          <Runtime minutes={movie.runTimeInMinutes} />
          <span>{movie.isAdult ? '18+' : 'All ages'}</span>
        </div>

        <div className='space-y-2'>
          <p className='line-clamp-3'>{movie.plot}</p>
          <div>Genres: {movie.genres?.join(', ')}</div>

          <MovieRatings
            averageRating={movie.averageRating}
            numberOfVotes={movie.numberOfVotes}
          />
        </div>
      </div>
    </Link>
  )
}
