import { Link } from "react-router";
import { useUser } from "../shared/userContext";

export const Header = () => {
  const [user] = useUser();

  return (
    <header>
      <h2>IMDB</h2>

      <nav>
        <Link to="/">Home</Link>
        <Link to="/movies">Movies</Link>
        <Link to="/people">People</Link>

        {user?.token ? (
          <>
            <Link to="/search">Search</Link>
            <span>Welcome, {user.name}</span>
            <Link to="/logout">Sign out</Link>
          </>
        ) : (
          <Link to="/login">Sign in</Link>
        )}
      </nav>
    </header>
  );
};
