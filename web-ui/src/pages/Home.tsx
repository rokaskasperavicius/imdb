import { useEffect } from 'react'

export const Home = () => {
  useEffect(() => {
    fetch('/api/movies')
      .then((res) => res.json())
      .then((data) => {
        console.log(data)
      })
  }, [])

  return (
    <div>
      <h2>Home Page</h2>
    </div>
  )
}
