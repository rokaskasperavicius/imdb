import type { paths } from '@/schema'

export type AllBookmarks =
  paths['/api/Bookmarks']['get']['responses']['200']['content']['application/json']
