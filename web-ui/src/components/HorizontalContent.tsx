import React from 'react'
import { Link } from 'react-router'

type Props = {
  children: React.ReactNode
}

type ItemProps = {
  to: string
  children: React.ReactNode
}

export const HorizontalContent = ({ children }: Props) => (
  <div className='flex gap-4 overflow-x-auto pb-2'>{children}</div>
)

export const HorizontalContentItem = ({ to, children }: ItemProps) => (
  <Link className='w-28 flex-0 shrink-0 basis-auto' to={to}>
    {children}
  </Link>
)
