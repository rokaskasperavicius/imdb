import { Navigate } from "react-router";
import { Search } from "../features/search/components/Search";
import { useUser } from "../shared/userContext";

export const SearchPage = () => {
  const [user] = useUser();

  if (!user?.token) {
    return <Navigate to="/login" replace />;
  }

  return (
    <div>
      <h2>Search Your Favorite Movies or Actors</h2>

      <Search token={user.token} />
    </div>
  );
};
