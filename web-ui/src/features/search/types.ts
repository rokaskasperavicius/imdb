import type { paths } from '@/types/schema'

export type AllSearches =
  paths['/api/Search']['get']['responses']['200']['content']['application/json']

export type HistorySearch = AllSearches[number]
