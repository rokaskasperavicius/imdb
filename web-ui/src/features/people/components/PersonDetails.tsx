import React, { useEffect, useState } from 'react'

import {
  HorizontalContent,
  HorizontalContentItem,
} from '@/components/HorizontalContent'
import { Image } from '@/components/Image'
import { Loader } from '@/components/Loader'

import { usePersonPoster } from '@/shared/usePersonPoster'

import { fetchPerson, fetchRelatedPeople } from '../api'
import type {
  PersonDetails as PersonDetailsType,
  RelatedPeople,
} from '../types'

type Props = {
  id: string
  knownForMovies: (movieIds: string[]) => React.ReactElement
}

export const PersonDetails = ({ id, knownForMovies }: Props) => {
  const url = usePersonPoster(id)
  const [person, setPerson] = useState<PersonDetailsType>()
  const [relatedPeople, setRelatedPeople] = useState<RelatedPeople>()

  useEffect(() => {
    const load = async () => {
      const personData = await fetchPerson(id)
      const relatedPeopleData = await fetchRelatedPeople(id)

      setPerson(personData)
      setRelatedPeople(relatedPeopleData)
    }

    load()
  }, [id])

  return (
    <div className='space-y-4'>
      <Loader data={person} type='vertical-narrow'>
        {(loaded) => (
          <div>
            <Image
              src={url || 'https://placehold.co/300x444'}
              alt={person?.primaryName || 'Person Poster'}
              className='w-auto h-auto'
            />

            <div>
              <h2>{loaded.primaryName}</h2>

              <div>{loaded.birthYear && `Born: ${loaded.birthYear}`}</div>
              <div>{loaded.deathYear && `Died: ${loaded.deathYear}`}</div>
              <div>Professions: {loaded.professions?.join(', ')}</div>
            </div>
          </div>
        )}
      </Loader>

      {person?.knownForMovies?.length && person.knownForMovies.length > 0
        ? knownForMovies(person.knownForMovies)
        : null}

      <Loader type='horizontal' data={relatedPeople}>
        {(loaded) => (
          <div>
            <h3>Related people</h3>

            <HorizontalContent>
              {loaded.map((person) => (
                <RelatedPerson key={person.id} person={person} />
              ))}
            </HorizontalContent>
          </div>
        )}
      </Loader>
    </div>
  )
}

export const RelatedPerson = ({
  person,
}: {
  person: RelatedPeople[number]
}) => {
  const url = usePersonPoster(person.id)

  return (
    <HorizontalContentItem key={person.id} to={`/people/${person.id}`}>
      <Image src={url} alt={person?.primaryName || 'Person Poster'} />
      <div>{person.primaryName}</div>
    </HorizontalContentItem>
  )
}
