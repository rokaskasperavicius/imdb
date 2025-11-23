import { Link } from 'react-router'

import { useUser } from '@/shared/userContext'

export const Header = () => {
  const [user] = useUser()

  return (
    <header className='flex justify-between items-center py-2.5 px-5 '>
      <h1>IMDB</h1>

      <nav className='flex gap-4'>
        <Link to='/'>Home</Link>
        <Link to='/movies'>Movies</Link>
        <Link to='/people'>People</Link>

        {user?.token ? (
          <>
            <Link to='/search'>Search</Link>
            <span>Welcome, {user.name}</span>
            <Link to='/logout'>Sign out</Link>
          </>
        ) : (
          <Link to='/login'>Sign in</Link>
        )}
      </nav>
    </header>
  )
}
