import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router'

import { Loader } from '@/components/Loader'

import { fetchBookmarks } from '../api'
import type { AllBookmarks } from '../types'

type Props = {
  token: string
  movies: (movieIds: string[]) => React.ReactNode
}

export const Bookmarks = ({ token, movies }: Props) => {
  const navigate = useNavigate()
  const [userBookmarks, setUserBookmarks] = useState<AllBookmarks>()

  useEffect(() => {
    const load = async () => {
      const data = await fetchBookmarks(token, navigate)
      setUserBookmarks(data)
    }

    load()
  }, [token, navigate])

  return (
    <Loader data={userBookmarks} type='vertical'>
      {(loaded) => movies(loaded.map((bookmark) => bookmark.id))}
    </Loader>
  )
}
