export const Runtime = ({
  minutes,
}: {
  minutes: number | null | undefined;
}) => {
  if (!minutes) {
    return null;
  }

  const hours = Math.floor(minutes / 60);
  const minutesLeft = minutes % 60;

  return (
    <span>
      {hours}h {minutesLeft}m
    </span>
  );
};
