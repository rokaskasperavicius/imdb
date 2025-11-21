import { useEffect, useState } from "react";
import { fetchPeople } from "../api";
import type { AllPeopleType } from "../types";
import { Loader } from "../../../shared/components/Loader";
import Pagination from "@mui/material/Pagination";
import { Person } from "./Person";

export const People = () => {
  const [page, setPage] = useState(1);
  const handleChange = (event: React.ChangeEvent<unknown>, value: number) => {
    setPage(value);
  };
  const [people, setPeople] = useState<AllPeopleType>();

  useEffect(() => {
    const load = async () => {
      const data = await fetchPeople(page);
      setPeople(data);
    };

    load();
  }, [page]);

  return (
    <div>
      <h2>People</h2>

      <div role="list" className="people">
        <Loader data={people}>
          {(loaded) =>
            loaded.data?.map((person, index) => (
              <Person
                key={person.id}
                person={person}
                personCount={(loaded.page - 1) * loaded.pageSize + index + 1}
              />
            ))
          }
        </Loader>
      </div>

      {people && (
        <Pagination
          className="people-pagination"
          count={people.totalPages}
          page={page}
          onChange={handleChange}
        />
      )}
    </div>
  );
};
