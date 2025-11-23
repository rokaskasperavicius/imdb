import { useEffect, useState } from 'react'
import { Link } from 'react-router'

import { Image } from '@/components/Image'
import { Loader } from '@/components/Loader'

import { usePersonPoster } from '@/shared/usePersonPoster'

import { fetchCast } from '../api'
import type { CastType } from '../types'

type Props = {
  movieId: string
}

export const Cast = ({ movieId }: Props) => {
  const [cast, setCast] = useState<CastType>()

  useEffect(() => {
    const load = async () => {
      const castData = await fetchCast(movieId)

      setCast(castData)
    }

    load()
  }, [movieId])

  return (
    <Loader data={cast} type='horizontal'>
      {(loaded) => (
        <div>
          <h3>Cast</h3>

          <div className='flex gap-4 overflow-x-auto pb-2'>
            {loaded
              .sort((a, b) => a.ordering - b.ordering)
              .map((cast) => (
                <Link
                  key={cast.personId}
                  className='w-32 flex-0 shrink-0 basis-auto'
                  to={`/people/${cast.personId}`}
                >
                  <CastImage cast={cast} />
                  <div>{cast.personName}</div>
                  <div>{cast.character}</div>
                </Link>
              ))}
          </div>
        </div>
      )}
    </Loader>
  )
}

export const CastImage = ({ cast }: { cast: CastType[number] }) => {
  const url = usePersonPoster(cast.personId)

  return (
    <Image
      src={url || 'https://placehold.co/100x176'}
      className='w-full h-[180px]'
      alt={cast.personName || 'Person Poster'}
    />
  )
}
