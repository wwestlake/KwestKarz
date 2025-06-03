import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { clearToken, isLoggedIn } from "../services/sessionService";

const IDLE_TIMEOUT_MS = 15 * 60 * 1000; // 15 minutes

export default function useIdleLogout() {
  const navigate = useNavigate();

  useEffect(() => {
    if (!isLoggedIn()) return;

    let timeout;

    const resetTimer = () => {
      clearTimeout(timeout);
      timeout = setTimeout(() => {
        clearToken();
        navigate("/login");
      }, IDLE_TIMEOUT_MS);
    };

    const events = ["mousemove", "keydown", "mousedown", "touchstart"];

    events.forEach((event) => window.addEventListener(event, resetTimer));
    resetTimer(); // start timer

    return () => {
      events.forEach((event) => window.removeEventListener(event, resetTimer));
      clearTimeout(timeout);
    };
  }, []);
}
