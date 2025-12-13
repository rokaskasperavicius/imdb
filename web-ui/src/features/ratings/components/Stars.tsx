import { useState } from 'react'

type StarsProps = {
  rating: number
  setRating: (rating: number) => void
}

export const Stars = ({ rating, setRating }: StarsProps) => {
  const [hover, setHover] = useState(0)

  return (
    <div className='flex gap-1'>
      {Array.from({ length: 10 }).map((_, index) => {
        const value = index + 1
        return (
          <button
            key={value}
            disabled={rating !== 0}
            onClick={() => setRating(value)}
            onMouseEnter={() => setHover(value)}
            onMouseLeave={() => setHover(0)}
          >
            <i
              className={`bi
                ${value <= (hover || rating) ? 'bi-star-fill text-amber-500' : 'bi-star'}`}
            ></i>
          </button>
        )
      })}
    </div>
  )
}
