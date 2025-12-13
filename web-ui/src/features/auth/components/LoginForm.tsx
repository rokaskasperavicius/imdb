import { useState } from 'react'
import { Link, useNavigate } from 'react-router'

import { Button } from '@/components/Button'
import { InputForm } from '@/components/InputForm'

import { useUser } from '@/hooks/userContext'

import { loginUser } from '../api'

export const LoginForm = () => {
  const [error, setError] = useState<string | null>(null)

  const [, setUser] = useUser()
  const navigate = useNavigate()

  const submit = async (e: React.FormEvent<HTMLFormElement>) => {
    setError(null)
    e.preventDefault()

    const formData = new FormData(e.currentTarget)
    const email = formData.get('email') as string
    const password = formData.get('password') as string

    const data = await loginUser(email, password)

    if (data.isError) {
      setError(data.message)
      return
    }

    setUser(() => ({
      name: data.name as string,
      token: data.accessToken as string,
    }))

    // Should probably navigate to page user came from
    // That logic is left for another time...
    navigate('/movies')
  }

  return (
    <form onSubmit={submit} className='space-y-4'>
      <div>
        <InputForm
          label='Email'
          id='email'
          name='email'
          type='email'
          required
        />

        <InputForm
          label='Password'
          id='password'
          name='password'
          type='password'
          required
        />

        {error && <p className='text-red-600'>{error}</p>}
      </div>

      <div className='space-y-2'>
        <Button type='submit'>Sign in</Button>

        <p>
          Don't have an account?{' '}
          <Link to='/register' className='underline!'>
            Register here
          </Link>
        </p>
      </div>
    </form>
  )
}
