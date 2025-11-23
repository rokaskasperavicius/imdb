type Props = {
  className?: string
} & React.ImgHTMLAttributes<HTMLImageElement>

export const Image = (props: Props) => (
  <img
    className={`${props.className} w-[100px] h-44 object-cover`}
    {...props}
  />
)
