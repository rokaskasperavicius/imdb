type Props = {
  averageRating: number
  numberOfVotes: number
}

export const MovieRatings = ({ averageRating, numberOfVotes }: Props) => {
  return (
    <div className='flex gap-2 items-center'>
      <div className='space-x-1'>
        <i className='bi bi-star-fill text-amber-500'></i>
        <span>{averageRating}/10</span>
      </div>

      <span>({numberOfVotes} votes)</span>
    </div>
  )
}
