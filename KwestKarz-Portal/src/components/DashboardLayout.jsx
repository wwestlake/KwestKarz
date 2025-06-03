import { NavLink } from "react-router-dom";
import useIdleLogout from "../hooks/useIdleLogout";

const menuItems = [
  { label: "Task", path: "/task" },
  { label: "Maintenance", path: "/maintenance" },
  { label: "Finance", path: "/finance" },
  { label: "Customer Relations", path: "/crm" },
  { label: "Settings", path: "/settings" },
  { label: "System Administration", path: "/admin" },
  { label: "Log Out", path: "/logout" }
];

export default function DashboardLayout({ children }) {
  useIdleLogout();

  return (
    <div className="flex min-h-screen bg-background text-text">
      <aside className="w-64 bg-primary p-4">
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
            >
              {label}
            </NavLink>
          ))}
        </nav>
      </aside>
      <main className="flex-1 p-6">{children}</main>
    </div>
  );
}
