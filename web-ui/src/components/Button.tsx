export const Button = ({
  children,
  ...props
}: React.ButtonHTMLAttributes<HTMLButtonElement>) => {
  return (
    <button className='w-full hover:bg-neutral-700' {...props}>
      {children}
    </button>
  )
}
