export const Button = ({
  children,
  ...props
}: React.ButtonHTMLAttributes<HTMLButtonElement>) => {
  return (
    <button
      className='w-full py-2 px-4 rounded-sm hover:bg-neutral-700 border border-root-text-color'
      {...props}
    >
      {children}
    </button>
  )
}
