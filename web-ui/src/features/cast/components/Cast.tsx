import { useEffect, useState } from 'react'

import {
  HorizontalContent,
  HorizontalContentItem,
} from '@/components/HorizontalContent'
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

          <HorizontalContent>
            {loaded
              .sort((a, b) => a.ordering - b.ordering)
              .map((cast) => (
                <HorizontalContentItem
                  key={cast.personId + cast.ordering}
                  to={`/people/${cast.personId}`}
                >
                  <CastImage cast={cast} />

                  <div>
                    <div>{cast.personName}</div>

                    <div className='text-sm text-neutral-400'>
                      {cast.character}
                    </div>

                    <div className='text-sm text-neutral-400 capitalize mt-0.5'>
                      {cast.category.split('_').join(' ')}
                    </div>
                  </div>
                </HorizontalContentItem>
              ))}
          </HorizontalContent>
        </div>
      )}
    </Loader>
  )
}

export const CastImage = ({ cast }: { cast: CastType[number] }) => {
  const url = usePersonPoster(cast.personId)

  return <Image src={url} alt={cast.personName || 'Person Poster'} />
}
