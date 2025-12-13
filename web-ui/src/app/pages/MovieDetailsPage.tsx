import { useParams } from 'react-router'

import { BookmarkMovie } from '@/features/bookmarks/components/BookmarkMovie'
import { Cast } from '@/features/cast/components/Cast'
import { MovieDetails } from '@/features/movies/components/MovieDetails'
import { RateMovie } from '@/features/ratings/components/RateMovie'

import { useUser } from '@/hooks/userContext'

export const MovieDetailsPage = () => {
  const [user] = useUser()
  const params = useParams()
  const movieId = params.movieId

  if (!movieId) {
    return null
  }

  return (
    <MovieDetails
      id={movieId}
      cast={<Cast movieId={movieId} />}
      rate={<RateMovie movieId={movieId} token={user?.token} />}
      bookmark={<BookmarkMovie movieId={movieId} token={user?.token} />}
    />
  )
}
