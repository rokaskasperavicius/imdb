import type { paths } from '@/types/schema'

export type AllBookmarks =
  paths['/api/Bookmarks']['get']['responses']['200']['content']['application/json']
