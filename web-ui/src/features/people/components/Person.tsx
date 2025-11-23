import { Link } from 'react-router'

import { Image } from '@/components/Image'

import { usePersonPoster } from '@/shared/usePersonPoster'

import type { PersonType } from '../types'

type Props = {
  person: PersonType
  personCount?: number
}

export const Person = ({ person, personCount }: Props) => {
  const url = usePersonPoster(person.id)

  return (
    <Link
      to={`/people/${person.id}`}
      className='hover:bg-neutral-700 flex gap-4'
      role='listitem'
    >
      <Image
        src={url || 'https://placehold.co/100x176'}
        alt={person?.primaryName || 'Person Poster'}
      />

      <div>
        <h4>
          {personCount}. {person.primaryName}
        </h4>

        <div>{person.birthYear && `Born: ${person.birthYear}`}</div>
        <div>{person.deathYear && `Died: ${person.deathYear}`}</div>
        <>Professions: {person.professions?.join(', ')}</>
      </div>
    </Link>
  )
}
