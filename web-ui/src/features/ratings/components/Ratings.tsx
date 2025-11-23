import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router'

import { Loader } from '@/components/Loader'

import { fetchRatings } from '../api'
import type { AllRatings } from '../types'

type Props = {
  token: string
  movies: (movieIds: string[]) => React.ReactNode
}

export const Ratings = ({ token, movies }: Props) => {
  const navigate = useNavigate()
  const [userRatings, setUserRatings] = useState<AllRatings>()

  useEffect(() => {
    const load = async () => {
      const data = await fetchRatings(token, navigate)
      setUserRatings(data)
    }

    load()
  }, [token, navigate])

  return (
    <Loader data={userRatings} type='horizontal'>
      {(loaded) => movies(loaded.map((rating) => rating.id))}
    </Loader>
  )
}
