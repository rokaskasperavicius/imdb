type Props = {
  src: string | null | undefined
  className?: string
} & Omit<React.ImgHTMLAttributes<HTMLImageElement>, 'src'>

export const Image = ({ src, className, ...props }: Props) => (
  <img
    className={`${className || ''} w-28 h-44 object-cover`}
    src={src || 'https://placehold.co/112x176'}
    {...props}
  />
)
