import { Link } from 'react-router'

import { useUser } from '@/shared/userContext'

export const Header = () => {
  const [user] = useUser()

  return (
    <header className='flex justify-between items-center py-2.5 px-5 gap-4'>
      <LinkItem to='/movies'>
        <h1>IMDB</h1>
      </LinkItem>

      <nav className='flex gap-4 overflow-x-auto'>
        <LinkItem to='/movies'>Movies</LinkItem>
        <LinkItem to='/people'>People</LinkItem>

        {user?.token ? (
          <>
            <LinkItem to='/search'>Search</LinkItem>

            <LinkItem to='/profile'>Profile</LinkItem>

            <LinkItem to='/logout'>Sign out</LinkItem>
          </>
        ) : (
          <LinkItem to='/login'>Sign in</LinkItem>
        )}
      </nav>
    </header>
  )
}

type LinkItemProps = {
  to: string
  children: React.ReactNode
}

const LinkItem = ({ to, children }: LinkItemProps) => (
  <Link to={to} className='hover:underline whitespace-nowrap'>
    {children}
  </Link>
)
