import usePagination from '@mui/material/usePagination'

import { PaginationButton } from './PaginationButton'

type Props = {
  page: number
  count?: number
  onChange: (page: number) => void
}

// Inspired from https://mui.com/material-ui/react-pagination/#usepagination
export const Pagination = ({ page, count = 0, onChange }: Props) => {
  const { items } = usePagination({
    count,
    page,
    onChange: (_, page) => onChange(page),
  })

  return (
    <nav className='flex justify-center text-sm'>
      <ul className='flex flex-wrap gap-2 items-center'>
        {items.map(({ page, type, selected, ...item }, index) => {
          let children = null

          if (type === 'start-ellipsis' || type === 'end-ellipsis') {
            children = (
              <div className='h-9 w-9 flex items-center justify-center'>…</div>
            )
          } else if (type === 'page') {
            children = (
              <PaginationButton
                className={selected ? 'bg-neutral-700' : ''}
                {...item}
              >
                {page}
              </PaginationButton>
            )
          } else {
            children = (
              <PaginationButton className='disabled:opacity-50' {...item}>
                {type === 'next' ? (
                  <i className='bi bi-chevron-right'></i>
                ) : (
                  <i className='bi bi-chevron-left'></i>
                )}
              </PaginationButton>
            )
          }

          return <li key={index}>{children}</li>
        })}
      </ul>
    </nav>
  )
}
