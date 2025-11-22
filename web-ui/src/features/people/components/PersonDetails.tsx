import React, { useEffect, useState } from "react";
import type {
  PersonDetails as PersonDetailsType,
  RelatedPeople,
} from "../types";
import { fetchPerson, fetchRelatedPeople } from "../api";
import { Loader } from "../../../components/Loader";
import { Link } from "react-router";
import { Image } from "../../../components/Image";
import { usePersonPoster } from "../../../shared/usePersonPoster";

type Props = {
  id: string;
  knownForMovies: (movieIds: string[]) => React.ReactElement;
};

export const PersonDetails = ({ id, knownForMovies }: Props) => {
  const url = usePersonPoster(id);
  const [person, setPerson] = useState<PersonDetailsType>();
  const [relatedPeople, setRelatedPeople] = useState<RelatedPeople>();

  useEffect(() => {
    const load = async () => {
      const personData = await fetchPerson(id);
      const relatedPeopleData = await fetchRelatedPeople(id);

      setPerson(personData);
      setRelatedPeople(relatedPeopleData);
    };

    load();
  }, [id]);

  return (
    <div className="space-y-4">
      <Loader data={person} type="vertical">
        {(loaded) => (
          <div>
            <Image
              src={url || "https://placehold.co/300x444"}
              alt={person?.primaryName || "Person Poster"}
              className="w-auto"
            />

            <div>
              <h4>{loaded.primaryName}</h4>

              <div>{loaded.birthYear && `Born: ${loaded.birthYear}`}</div>
              <div>{loaded.deathYear && `Died: ${loaded.deathYear}`}</div>
              <>Professions: {loaded.professions?.join(", ")}</>
            </div>
          </div>
        )}
      </Loader>

      {person?.knownForMovies?.length && person.knownForMovies.length > 0
        ? knownForMovies(person.knownForMovies)
        : null}

      <Loader type="horizontal" data={relatedPeople}>
        {(loaded) => (
          <div>
            <h3>Related People</h3>

            <div className="flex gap-4 overflow-x-auto pb-2">
              {loaded.map((person) => (
                <RelatedPerson key={person.id} person={person} />
              ))}
            </div>
          </div>
        )}
      </Loader>
    </div>
  );
};

export const RelatedPerson = ({
  person,
}: {
  person: RelatedPeople[number];
}) => {
  const url = usePersonPoster(person.id);

  return (
    <Link
      key={person.id}
      className="w-32 flex-0 shrink-0 basis-auto"
      to={`/people/${person.id}`}
    >
      <Image
        src={url || "https://placehold.co/100x150"}
        alt={person?.primaryName || "Person Poster"}
        className="w-full h-[180px]"
      />
      <div>{person.primaryName}</div>
    </Link>
  );
};
