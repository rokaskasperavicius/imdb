import { Navigate } from 'react-router'

import { Bookmarks } from '@/features/bookmarks/components/Bookmarks'
import { BatchMovies } from '@/features/movies/components/BatchMovies'
import { Ratings } from '@/features/ratings/components/Ratings'

import { useUser } from '@/shared/userContext'

export const ProfilePage = () => {
  const [user] = useUser()

  if (!user?.token) {
    return <Navigate to='/login' replace />
  }

  return (
    <div>
      <h2>Welcome, {user.name}!</h2>

      <div className='space-y-4'>
        <Bookmarks
          token={user.token}
          movies={(movieIds) => (
            <BatchMovies
              title='Your bookmarked movies'
              emptyTitle='Here you will see your bookmarked movies'
              movieIds={movieIds}
            />
          )}
        />

        <Ratings
          token={user.token}
          movies={(movieIds) => (
            <BatchMovies
              title='Your rated movies'
              emptyTitle='Here you will see your rated movies'
              movieIds={movieIds}
            />
          )}
        />
      </div>
    </div>
  )
}
