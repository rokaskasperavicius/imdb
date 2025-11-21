import { Link } from "react-router";
import type { PersonType } from "../types";
import { usePersonPoster } from "../utils/usePersonPoster";

type Props = {
  person: PersonType;
  personCount?: number;
};

export const Person = ({ person, personCount }: Props) => {
  const url = usePersonPoster(person.id);

  return (
    <Link to={`/people/${person.id}`} className="person" role="listitem">
      <img
        src={url || "https://placehold.co/300x444"}
        alt={person?.primaryName || "Person Poster"}
      />

      <div className="person-content">
        <h4>
          {personCount}. {person.primaryName}
        </h4>

        <div>{person.birthYear && `Born: ${person.birthYear}`}</div>
        <div>{person.deathYear && `Died: ${person.deathYear}`}</div>
        <>Professions: {person.professions?.join(", ")}</>
      </div>
    </Link>
  );
};
