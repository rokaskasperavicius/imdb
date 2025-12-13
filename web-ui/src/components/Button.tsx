import ButtonBootstrap from 'react-bootstrap/Button'

export const Button = ({
  children,
  ...props
}: React.ButtonHTMLAttributes<HTMLButtonElement>) => {
  return (
    <ButtonBootstrap className='w-full py-2 px-4 rounded-sm!' {...props}>
      {children}
    </ButtonBootstrap>
  )
}
