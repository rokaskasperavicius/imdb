type Loader<T> = {
  data: T | null | undefined
  type?: 'horizontal' | 'vertical' | 'default'
  children: (data: T) => React.ReactNode
}

export const Loader = <T,>({ data, type = 'default', children }: Loader<T>) => {
  if (data === null || data === undefined) {
    if (type === 'horizontal') {
      return <HorizontalSkeleton />
    }

    if (type === 'vertical') {
      return <VerticalSkeleton />
    }

    return null
  }

  return <>{children(data)}</>
}

const VerticalSkeleton = () => {
  return (
    <div className='space-y-4'>
      {Array.from({ length: 5 }).map((_, index) => (
        <div
          key={index}
          className='h-48 w-full bg-neutral-700 rounded animate-pulse'
        ></div>
      ))}
    </div>
  )
}

const HorizontalSkeleton = () => {
  return (
    <div className='flex gap-4 overflow-x-auto'>
      {Array.from({ length: 5 }).map((_, index) => (
        <div
          key={index}
          className='h-44 w-32 bg-neutral-700 rounded animate-pulse mb-2 flex-0 shrink-0 basis-auto'
        ></div>
      ))}
    </div>
  )
}
