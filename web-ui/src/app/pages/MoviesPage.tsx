import { useSearchParams } from 'react-router'

import { Movies } from '@/features/movies/components/Movies'

export const MoviesPage = () => {
  const [searchParams, setSearchParams] = useSearchParams()

  const pageQuery = searchParams.get('page')
  const page = parseInt(pageQuery || '1', 10)

  const handlePageChange = (page: number) => {
    setSearchParams({ page: page.toString() })
  }

  return (
    <div className='space-y-4'>
      <h2>Movies</h2>

      <Movies page={isNaN(page) ? 1 : page} onPageChange={handlePageChange} />
    </div>
  )
}
