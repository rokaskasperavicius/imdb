import { useSearchParams } from 'react-router'

import { People } from '@/features/people/components/People'

export const PeoplePage = () => {
  const [searchParams, setSearchParams] = useSearchParams()

  const pageQuery = searchParams.get('page')
  const page = parseInt(pageQuery || '1', 10)

  const handlePageChange = (page: number) => {
    setSearchParams({ page: page.toString() })
  }

  return (
    <div className='space-y-4'>
      <h2>People</h2>

      <People page={isNaN(page) ? 1 : page} onPageChange={handlePageChange} />
    </div>
  )
}
