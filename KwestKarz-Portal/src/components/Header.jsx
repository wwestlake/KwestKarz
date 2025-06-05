import { NavLink } from "react-router-dom";
import UserMenu from "./UserMenu";
import { isLoggedIn, onAuthChange } from "../services/sessionService";
import { useState, useEffect } from "react";

const navLinkClass = ({ isActive }) =>
  `block px-3 py-2 rounded-md text-sm font-medium ${
    isActive ? "bg-accent text-white" : "text-text hover:bg-secondary"
  }`;

export default function Header() {
  const [loggedIn, setLoggedIn] = useState(isLoggedIn());
  const [menuOpen, setMenuOpen] = useState(false);

  useEffect(() => {
    setLoggedIn(isLoggedIn());
    const unsubscribe = onAuthChange(() => setLoggedIn(isLoggedIn()));
    return unsubscribe;
  }, []);

  return (
    <header className="bg-primary shadow">
      <div className="max-w-7xl mx-auto px-4 py-3 flex flex-wrap justify-between items-center">
        <h1 className="text-xl font-bold text-white">Kwest Karz</h1>
        <button
          className="sm:hidden text-white text-xl"
          onClick={() => setMenuOpen(!menuOpen)}
        >
          ☰
        </button>
        <nav
          className={`w-full sm:w-auto sm:flex ${
            menuOpen ? "block" : "hidden"
          } sm:space-x-2 mt-2 sm:mt-0`}
        >
          <NavLink to="/" className={navLinkClass}>Home</NavLink>
          <NavLink to="/vehicles" className={navLinkClass}>Vehicles</NavLink>
          <NavLink to="/turo" className={navLinkClass}>Turo</NavLink>
          <NavLink to="/help" className={navLinkClass}>Help</NavLink>
          <NavLink to="/contact" className={navLinkClass}>Contact</NavLink>
          {loggedIn ? <UserMenu /> : <NavLink to="/login" className={navLinkClass}>Login</NavLink>}
        </nav>
      </div>
    </header>
  );
}
