import { useEffect, useState } from 'react'
import { type NavigateFunction, useNavigate } from 'react-router'

import { bookmarkMovie, deleteBookmark, fetchBookmarks } from '../api'
import type { AllBookmarks } from '../types'

type Props = {
  movieId: string
  token: string | null | undefined
}

export const BookmarkMovie = ({ movieId, token }: Props) => {
  const navigate = useNavigate()
  const [userBookmarks, setUserBookmarks] = useState<AllBookmarks>()

  useEffect(() => {
    const load = async (token: string) => {
      const data = await fetchBookmarks(token, navigate)
      setUserBookmarks(data)
    }

    if (token) load(token)
  }, [token, navigate, movieId])

  // Nothing on loading
  if (!userBookmarks && token) {
    return null
  }

  const existingBookmark = !!userBookmarks?.find(
    (bookmark) => bookmark.id === movieId,
  )

  return (
    <BookmarkMovieComponent
      // key allows to re-render the component when movieId changes
      key={movieId}
      movieId={movieId}
      token={token}
      navigate={navigate}
      existingBookmark={existingBookmark}
    />
  )
}

type BookmarkMovieComponentProps = {
  movieId: string
  token: string | null | undefined
  existingBookmark: boolean
  navigate: NavigateFunction
}

const BookmarkMovieComponent = ({
  movieId,
  token,
  existingBookmark,
  navigate,
}: BookmarkMovieComponentProps) => {
  const [bookmarked, setBookmarked] = useState(existingBookmark)

  const handleClick = async () => {
    if (!token) {
      navigate('/login')
      return
    }

    if (!bookmarked) {
      await handleBookmark(token)
    } else {
      await handleRemoveBookmark(token)
    }
  }

  const handleBookmark = async (token: string) => {
    await bookmarkMovie(movieId, token, navigate)
    setBookmarked(true)
  }

  const handleRemoveBookmark = async (token: string) => {
    await deleteBookmark(movieId, token, navigate)
    setBookmarked(false)
  }

  return (
    <button className='bookmark-container' onClick={handleClick}>
      {bookmarked && <i className='bi bookmark-filled'></i>}

      {!bookmarked && <i className='bi bookmark'></i>}
    </button>
  )
}
