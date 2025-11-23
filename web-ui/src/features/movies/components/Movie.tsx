import { Link } from 'react-router'

import { Image } from '@/components/Image'

import type { MovieType } from '../types'
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
      <Image
        src={movie.poster || 'https://placehold.co/300x444'}
        alt={movie.title}
      />

      <div>
        <h4>
          {titleCount}. {movie.title}
        </h4>

        <div className='flex gap-4'>
          <span>{movie.year}</span>
          <Runtime minutes={movie.runTimeInMinutes} />
          <span>{movie.isAdult ? '18+' : 'All ages'}</span>
        </div>

        <div className='space-y-2'>
          <p className='line-clamp-3'>{movie.plot}</p>
          <div>Genres: {movie.genres?.join(', ')}</div>

          <div>
            {movie.averageRating} ({movie.numberOfVotes})
          </div>
        </div>
      </div>
    </Link>
  )
}
