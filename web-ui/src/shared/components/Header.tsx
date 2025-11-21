import { Link } from "react-router";
import { useUser } from "../../features/auth/useUser";

export const Header = () => {
  const { user } = useUser();

  return (
    <header>
      <h2>IMDB</h2>

      <nav>
        <Link to="/">Home</Link>
        <Link to="/movies">Movies</Link>
        <Link to="/people">People</Link>

        {user?.token ? (
          <>
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
