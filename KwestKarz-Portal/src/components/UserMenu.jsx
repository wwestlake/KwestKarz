import { useState, useEffect, useRef } from "react";
import { getClaims, clearToken } from "../services/sessionService";
import { useNavigate } from "react-router-dom";

export default function UserMenu() {
  console.log("UserMenu claims:", getClaims());
  const [open, setOpen] = useState(false);
  const [initials, setInitials] = useState("UU");
  const menuRef = useRef();
  const navigate = useNavigate();

  useEffect(() => {
    const claims = getClaims();
    if (claims?.email) {
      const [name] = claims.email.split("@");
      const parts = name.split(".");
      const initials = parts.map(p => p[0]).join("").toUpperCase().slice(0, 2);
      setInitials(initials);
    }
  }, []);

  useEffect(() => {
    function handleClickOutside(e) {
      if (menuRef.current && !menuRef.current.contains(e.target)) {
        setOpen(false);
      }
    }
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  function handleLogout() {
    clearToken();
    navigate("/login");
  }

  return (
    <div className="relative" ref={menuRef}>
      <button
        onClick={() => setOpen(!open)}
        className="w-10 h-10 rounded-full bg-accent text-white flex items-center justify-center font-bold"
      >
        {initials}
      </button>
      {open && (
        <div className="absolute right-0 mt-2 w-48 bg-white shadow-lg rounded-md z-10 text-sm">
          <button
                onClick={() => navigate("/change-password")}
                    className="block w-full text-left px-4 py-2 hover:bg-gray-100 text-black"
            >
                Change Password
          </button>
          <button
            onClick={handleLogout}
            className="block w-full text-left px-4 py-2 hover:bg-gray-100 text-red-600"
          >
            Log Out
          </button>
        </div>
      )}
    </div>
  );
}
