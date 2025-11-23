import type { HistorySearch } from '../types'

type Props = {
  item: HistorySearch
  onClick: () => void
}

export const SearchHistoryItem = ({ item, onClick }: Props) => (
  <button
    role='listitem'
    className='cursor-pointer flex gap-2 hover:bg-neutral-700 p-1 text-start w-full'
    onClick={onClick}
  >
    <i className='bi bi-clock-history mt-0.5'></i>

    <div>
      <h4>{item.query}</h4>
      <p className='text-neutral-400 text-sm'>
        Searched on: {new Date(item.createdAt).toLocaleString()}
      </p>
    </div>
  </button>
)
