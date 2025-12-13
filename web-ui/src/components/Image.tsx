import ImageBootstrap from 'react-bootstrap/Image'

type Props = {
  src: string | null | undefined
  className?: string
} & Omit<React.ImgHTMLAttributes<HTMLImageElement>, 'src'>

export const Image = ({ src, className, ...props }: Props) => (
  <ImageBootstrap
    className={`${className || ''} object-cover`}
    width={112}
    height={176}
    src={src || 'https://placehold.co/112x176'}
    {...props}
  />
)
