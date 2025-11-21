import { useEffect, useState } from "react";
import { MOVIE_API_KEY } from "../../../constants";

// Simple cache so we don't call themoviedb API multiple times for the same person
const cache = new Map<string, string | null>();

export const usePersonPoster = (personId: string) => {
  const [url, setUrl] = useState<string | null>(null);

  useEffect(() => {
    const load = async () => {
      try {
        if (cache.has(personId)) {
          setUrl(cache.get(personId) || null);
          return;
        }

        const response = await fetch(
          `https://api.themoviedb.org/3/find/${personId}?external_source=imdb_id&api_key=${MOVIE_API_KEY}`
        );
        const data = await response.json();
        let posterUrl = null;

        if (data?.person_results && data.person_results.length > 0) {
          const personData = data.person_results[0];

          if (personData?.profile_path) {
            posterUrl = `https://image.tmdb.org/t/p/w300${personData.profile_path}`;
            setUrl(posterUrl);
          }
        }

        cache.set(personId, posterUrl);
      } catch (error) {
        console.error("Error fetching person poster:", error);
      }
    };

    load();
  }, [personId]);

  return url;
};
