import { createContext, useContext } from "react";
import { useLocalStorage } from "react-use";

type UserType = {
  name: string | null;
  token: string | null;
};

type UserContextType = ReturnType<typeof useLocalStorage<UserType>>;

const UserContext = createContext<UserContextType | null>(null);

export const UserProvider = ({ children }: { children: React.ReactNode }) => {
  const value = useLocalStorage<UserType>("user", {
    name: null,
    token: null,
  });

  return <UserContext.Provider value={value}>{children}</UserContext.Provider>;
};

// eslint-disable-next-line react-refresh/only-export-components
export const useUser = () => {
  const ctx = useContext(UserContext);

  if (!ctx) {
    throw new Error("useUser must be used within <UserProvider>");
  }

  return ctx;
};
