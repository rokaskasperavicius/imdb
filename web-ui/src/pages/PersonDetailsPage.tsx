import { useParams } from 'react-router'

import { KnownForMovies } from '@/features/movies/components/KnownForMovies'
import { PersonDetails } from '@/features/people/components/PersonDetails'

export const PersonDetailsPage = () => {
  const params = useParams()
  const personId = params.personId

  if (!personId) {
    return null
  }

  return (
    <PersonDetails
      id={personId}
      knownForMovies={(movieIds) => <KnownForMovies movieIds={movieIds} />}
    />
  )
}
