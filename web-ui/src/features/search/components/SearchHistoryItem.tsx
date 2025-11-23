import type { HistorySearch } from '../types'

type Props = {
  item: HistorySearch
}

export const SearchHistoryItem = ({ item }: Props) => (
  <div className='p-3 border-b border-neutral-600'>
    <h3 className='text-lg font-semibold'>{item.query}</h3>
    <p className='text-sm text-neutral-400'>
      Searched on: {new Date(item.createdAt).toLocaleString()}
    </p>
  </div>
)
