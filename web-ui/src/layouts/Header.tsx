import Nav from 'react-bootstrap/Nav'
import NavDropdown from 'react-bootstrap/NavDropdown'
import Navbar from 'react-bootstrap/Navbar'

import { useUser } from '@/hooks/userContext'

export const Header = () => {
  const [user] = useUser()

  return (
    <header className='px-5'>
      <Navbar expand='sm' className='w-full'>
        <Navbar.Brand href='/movies'>IMDB</Navbar.Brand>
        <Navbar.Toggle aria-controls='basic-navbar-nav' />

        <Navbar.Collapse id='basic-navbar-nav'>
          <Nav className='w-full justify-end'>
            <Nav.Link href='/movies'>Movies</Nav.Link>
            <Nav.Link href='/people'>People</Nav.Link>
            {user?.token ? (
              <>
                <Nav.Link href='/search'>Search</Nav.Link>
                <Nav.Link href='/profile'>Profile</Nav.Link>
                <NavDropdown title='Account'>
                  <NavDropdown.Item href='/logout'>Sign out</NavDropdown.Item>
                </NavDropdown>
              </>
            ) : (
              <Nav.Link href='/login'>Sign in</Nav.Link>
            )}
          </Nav>
        </Navbar.Collapse>
      </Navbar>
    </header>
  )
}
