type Props = {
  label: string;
} & React.InputHTMLAttributes<HTMLInputElement>;

export const InputForm = ({ label, ...props }: Props) => {
  return (
    <div className="input-form">
      <label htmlFor={props.id}>{label}</label>
      <input {...props} />
    </div>
  );
};
