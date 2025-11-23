import { useState } from 'react'
import { useNavigate } from 'react-router'

import { Button } from '@/components/Button'
import { InputForm } from '@/components/InputForm'

import { registerUser } from '../api'

export const RegisterForm = () => {
  const [error, setError] = useState<string | null>(null)
  const navigate = useNavigate()

  const submit = async (e: React.FormEvent<HTMLFormElement>) => {
    setError(null)
    e.preventDefault()

    const formData = new FormData(e.currentTarget)
    const name = formData.get('name') as string
    const email = formData.get('email') as string
    const password = formData.get('password') as string
    const confirmPassword = formData.get('confirmPassword') as string

    if (password !== confirmPassword) {
      setError('Passwords do not match')
      return
    }

    const data = await registerUser(name, email, password)

    if (data.isError) {
      setError(data.message)
      return
    }

    navigate('/login')
  }

  return (
    <form onSubmit={submit} className='space-y-4'>
      <div>
        <InputForm label='Name' id='name' name='name' type='text' required />

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

        <InputForm
          label='Confirm Password'
          id='confirmPassword'
          name='confirmPassword'
          type='password'
          required
        />

        {error && <p className='text-red-600'>{error}</p>}
      </div>

      <Button type='submit'>Sign up</Button>
    </form>
  )
}
