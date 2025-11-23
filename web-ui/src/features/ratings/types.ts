import type { paths } from '@/schema'

export type AllRatings =
  paths['/api/Ratings']['get']['responses']['200']['content']['application/json']
