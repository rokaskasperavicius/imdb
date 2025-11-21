type Loader<T> = {
  data: T | null | undefined;
  children: (data: T) => React.ReactNode;
};

export const Loader = <T,>({ data, children }: Loader<T>) => {
  if (data === null || data === undefined) {
    return <>Loading...</>;
  }

  return <>{children(data)}</>;
};
