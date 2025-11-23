import { useParams } from 'react-router'

import { BatchMovies } from '@/features/movies/components/BatchMovies'
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
      knownForMovies={(movieIds) => (
        <BatchMovies
          title='Known for movies'
          emptyTitle=''
          movieIds={movieIds}
        />
      )}
    />
  )
}
