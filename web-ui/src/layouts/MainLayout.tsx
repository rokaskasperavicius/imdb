import { Outlet } from 'react-router'

import { Header } from './Header'

export const MainLayout = () => {
  return (
    <div className='flex flex-col min-h-screen'>
      <Header />

      <div className='flex-1 p-5'>
        <Outlet />
      </div>
    </div>
  )
}
