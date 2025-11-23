import type { paths } from '@/schema'

export type AllMovies =
  paths['/api/Movies']['get']['responses']['200']['content']['application/json']

export type AllMoviesBatch =
  paths['/api/Movies/batch']['get']['responses']['200']['content']['application/json']

export type MovieType = NonNullable<AllMovies['data']>[number]

export type MovieDetails =
  paths['/api/Movies/{tconst}']['get']['responses']['200']['content']['application/json']

export type RelatedMovies =
  paths['/api/Movies/{tconst}/related']['get']['responses']['200']['content']['application/json']
