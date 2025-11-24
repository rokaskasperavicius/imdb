type Props = {
  children: React.ReactNode
} & React.ButtonHTMLAttributes<HTMLButtonElement>

export const PaginationButton = ({ className, children, ...props }: Props) => (
  <button
    className={`hover:bg-neutral-700 rounded-xl px-2 h-9 min-w-9 ${className || ''}`}
    {...props}
  >
    {children}
  </button>
)
