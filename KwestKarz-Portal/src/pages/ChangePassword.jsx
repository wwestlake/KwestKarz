// pages/ChangePassword.jsx
import { useState } from "react";
import { changePassword } from "../services/userService";

export default function ChangePassword() {
  const [form, setForm] = useState({ currentPassword: "", newPassword: "", confirmPassword: "" });
  const [status, setStatus] = useState("");

  async function handleSubmit(e) {
    e.preventDefault();
    setStatus("");

    if (form.newPassword !== form.confirmPassword) {
      setStatus("New passwords do not match.");
      return;
    }

    try {
      await changePassword(form.currentPassword, form.newPassword);
      setStatus("Password changed successfully.");
      setForm({ currentPassword: "", newPassword: "", confirmPassword: "" });
    } catch (err) {
      setStatus("Password change failed.");
    }
  }

  return (
    <form onSubmit={handleSubmit} className="max-w-md mx-auto p-4">
      <h2 className="text-xl font-bold mb-4">Change Password</h2>

      <input
        type="password"
        placeholder="Current Password"
        value={form.currentPassword}
        onChange={(e) => setForm({ ...form, currentPassword: e.target.value })}
        required
        autoComplete="current-password"
        className="w-full mb-4 p-2 border rounded bg-background text-text"
      />

      <input
        type="password"
        placeholder="New Password"
        value={form.newPassword}
        onChange={(e) => setForm({ ...form, newPassword: e.target.value })}
        required
        autoComplete="new-password"
        className="w-full mb-4 p-2 border rounded bg-background text-text"
      />

      <input
        type="password"
        placeholder="Confirm Password"
        value={form.confirmPassword}
        onChange={(e) => setForm({ ...form, confirmPassword: e.target.value })}
        required
        autoComplete="new-password"
        className="w-full mb-4 p-2 border rounded bg-background text-text"
      />

      <button type="submit" className="bg-accent text-white px-4 py-2 rounded">
        Change Password
      </button>

      {status && <p className="mt-2 text-sm">{status}</p>}
    </form>
  );
}
