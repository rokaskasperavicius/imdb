import { useLocalStorage } from "react-use";

type UserType = {
  name: string | null;
  token: string | null;
};

export const useUser = () => {
  const [user, setUser] = useLocalStorage<UserType>("user", {
    name: null,
    token: null,
  });

  return { user, setUser };
};
