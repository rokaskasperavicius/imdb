import { useNavigate } from "react-router";
import { loginUser } from "../api";
import { useState } from "react";
import { useUser } from "../../../shared/userContext";

export const LoginForm = () => {
  const [error, setError] = useState<string | null>(null);

  const [, setUser] = useUser();
  const navigate = useNavigate();

  const submit = async (e: React.FormEvent<HTMLFormElement>) => {
    setError(null);
    e.preventDefault();

    const formData = new FormData(e.currentTarget);
    const email = formData.get("email") as string;
    const password = formData.get("password") as string;

    const data = await loginUser(email, password);

    if (data.isError) {
      setError(data.message);
      return;
    }

    setUser(() => ({
      name: data.name as string,
      token: data.accessToken as string,
    }));

    navigate("/");
  };

  return (
    <form onSubmit={submit}>
      <div>
        <label htmlFor="email">Email:</label>
        <input type="email" id="email" name="email" required />
      </div>

      <div>
        <label htmlFor="password">Password:</label>
        <input type="password" id="password" name="password" required />
      </div>

      {error && <div className="error">{error}</div>}

      <button type="submit">Sign in</button>
    </form>
  );
};
