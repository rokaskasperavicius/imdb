import { useEffect, useState } from 'react'
import { type NavigateFunction, useNavigate } from 'react-router'

import { fetchRatings, rateMovie } from '../api'
import type { AllRatings } from '../types'
import { Stars } from './Stars'

type Props = {
  movieId: string
  token: string | null | undefined
}

export const RateMovie = ({ movieId, token }: Props) => {
  const navigate = useNavigate()
  const [userRatings, setUserRatings] = useState<AllRatings>()

  useEffect(() => {
    const load = async (token: string) => {
      const data = await fetchRatings(token, navigate)
      setUserRatings(data)
    }

    if (token) load(token)
  }, [token, navigate, movieId])

  // Nothing on loading
  if (!userRatings && token) {
    return null
  }

  const existingRating = userRatings?.find(
    (rating) => rating.id === movieId,
  )?.rating

  return (
    <RateMovieComponent
      // key allows to re-render the component when movieId changes
      key={movieId}
      movieId={movieId}
      token={token}
      navigate={navigate}
      userRating={existingRating}
    />
  )
}

type RateMovieComponentProps = {
  movieId: string
  token: string | null | undefined
  userRating?: number
  navigate: NavigateFunction
}

const RateMovieComponent = ({
  movieId,
  token,
  userRating,
  navigate,
}: RateMovieComponentProps) => {
  const [rating, setRating] = useState(userRating || 0)
  const [showStars, setShowStars] = useState(!!userRating)

  const handleRateMovie = async (rating: number) => {
    if (!token) {
      navigate('/login')
      return
    }

    setRating(rating)
    await rateMovie(movieId, rating, token, navigate)
  }

  if (showStars) {
    return (
      <div>
        Your vote:
        <Stars rating={rating} setRating={handleRateMovie} />
      </div>
    )
  }

  return (
    <button
      onClick={() => setShowStars((b) => !b)}
      className='rate-star-container flex gap-1 items-center group'
    >
      <i className='rate-star bi group-hover:text-amber-500'></i>

      <span>Rate this movie</span>
    </button>
  )
}
