import { useEffect } from 'react'
import { Navigate } from 'react-router'

import { useUser } from '@/hooks/userContext'

export const LogoutPage = () => {
  const [, setUser] = useUser()

  useEffect(() => {
    setUser(() => ({ name: null, token: null }))
  }, [setUser])

  return <Navigate to='/login' replace />
}
