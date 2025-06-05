import { useState } from "react";
import { NavLink } from "react-router-dom";
import useIdleLogout from "../hooks/useIdleLogout";

const menuItems = [
  { label: "Task", path: "/task" },
  { label: "Maintenance", path: "/maintenance" },
  { label: "Finance", path: "/finance" },
  { label: "Customer Relations", path: "/crm" },
  { label: "Settings", path: "/settings" },
  { label: "System Administration", path: "/admin" },
  { label: "User Management", path: "/dashboard/users" },
  { label: "Invite User", path: "/dashboard/users/invite" },
  { label: "Reports", path: "/reports" },
  { label: "Analytics", path: "/analytics" },
  { label: "Notifications", path: "/notifications" },
  { label: "Help & Support", path: "/help" },
  { label: "Log Out", path: "/logout" }
];

export default function DashboardLayout({ children }) {
  useIdleLogout();
  const [isOpen, setIsOpen] = useState(false);

  return (
    <div className="flex min-h-screen bg-background text-text">
      {/* Mobile toggle button */}
      <button
        onClick={() => setIsOpen(!isOpen)}
        className="sm:hidden fixed top-4 left-4 z-50 bg-accent text-white p-2 rounded shadow"
      >
        ☰
      </button>

      {/* Sidebar */}
      <aside
        className={`fixed sm:static top-0 left-0 h-full w-64 bg-primary p-4 z-40 transform transition-transform duration-300 ${
          isOpen ? "translate-x-0" : "-translate-x-full sm:translate-x-0"
        }`}
      >
        <h2 className="text-xl font-bold text-accent mb-6">Menu</h2>
        <nav className="space-y-2">
          {menuItems.map(({ label, path }) => (
            <NavLink
              key={path}
              to={path}
              className={({ isActive }) =>
                `block px-4 py-2 rounded ${
                  isActive ? "bg-accent text-white" : "hover:bg-secondary"
                }`
              }
              onClick={() => setIsOpen(false)} // close on nav click
            >
              {label}
            </NavLink>
          ))}
        </nav>
      </aside>

      {/* Main content */}
      <main className="flex-1 p-6 sm:ml-64">{children}</main>
    </div>
  );
}
