import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { processLogin } from "../services/sessionCoordinator";

export default function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [loggedIn, setLoggedIn] = useState(false);
  const navigate = useNavigate();

  async function handleSubmit(e) {
    e.preventDefault();
    setError("");

    const result = await processLogin(username, password);

    if (result.success) {
      setLoggedIn(true);
      setTimeout(() => {
        navigate("/dashboard");
      }, 5000);
    } else {
      setError(result.error);
    }
  }

  return (
    <div className="max-w-md mx-auto mt-20 p-6 bg-secondary rounded shadow">
      <h2 className="text-center text-2xl font-bold text-accent mb-6">
        Employees Only
      </h2>

      {loggedIn ? (
        <p className="text-center text-blue-400 font-semibold">You are logged in</p>
      ) : (
        <>
          {error && <p className="text-red-500 mb-4 text-center">{error}</p>}
          <form onSubmit={handleSubmit} className="space-y-4">
            <div>
              <label htmlFor="username" className="block text-sm mb-1 text-text">
                User Name
              </label>
              <input
                type="text"
                id="username"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                className="w-full px-4 py-2 rounded bg-background text-text border border-gray-600 focus:outline-none focus:ring focus:ring-accent"
              />
            </div>
            <div>
              <label htmlFor="password" className="block text-sm mb-1 text-text">
                Password
              </label>
              <input
                type="password"
                id="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                className="w-full px-4 py-2 rounded bg-background text-text border border-gray-600 focus:outline-none focus:ring focus:ring-accent"
              />
            </div>
            <button
              type="submit"
              className="w-full py-2 bg-accent text-white font-semibold rounded hover:bg-opacity-90"
            >
              Login
            </button>
          </form>
        </>
      )}
    </div>
  );
}
