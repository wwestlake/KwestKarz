import { useState } from "react";
import { useSearchParams } from "react-router-dom";
import config from "../config";

export default function CompleteAccountSetup() {
  const [params] = useSearchParams();
  const token = params.get("token");

  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    password: "",
    confirmPassword: ""
  });
  const [status, setStatus] = useState("");

  async function handleSubmit(e) {
    e.preventDefault();
    setStatus("");

    if (form.password !== form.confirmPassword) {
      setStatus("Passwords do not match.");
      return;
    }

    const { confirmPassword, ...payload } = form;

    const res = await fetch(`${config.apiBaseUrl}/api/Auth/setup`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ ...payload, token })
    });

    setStatus(res.ok ? "Setup complete!" : "Setup failed.");
  }

  return (
    <form onSubmit={handleSubmit} className="max-w-md mx-auto p-4 text-text">
      <h2 className="text-xl font-bold mb-4">Complete Account Setup</h2>

      <label className="block mb-1">First Name</label>
      <input
        type="text"
        placeholder="First Name"
        value={form.firstName}
        onChange={(e) => setForm({ ...form, firstName: e.target.value })}
        required
        className="w-full mb-4 p-2 border rounded bg-background text-text"
      />

      <label className="block mb-1">Last Name</label>
      <input
        type="text"
        placeholder="Last Name"
        value={form.lastName}
        onChange={(e) => setForm({ ...form, lastName: e.target.value })}
        required
        className="w-full mb-4 p-2 border rounded bg-background text-text"
      />

      <label className="block mb-1">Password</label>
      <input
        type="password"
        placeholder="Password"
        value={form.password}
        onChange={(e) => setForm({ ...form, password: e.target.value })}
        required
        className="w-full mb-4 p-2 border rounded bg-background text-text"
      />

      <label className="block mb-1">Confirm Password</label>
      <input
        type="password"
        placeholder="Confirm Password"
        value={form.confirmPassword}
        onChange={(e) => setForm({ ...form, confirmPassword: e.target.value })}
        required
        className="w-full mb-4 p-2 border rounded bg-background text-text"
      />

      <button type="submit" className="bg-accent text-white px-4 py-2 rounded">
        Complete Setup
      </button>
      {status && <p className="mt-2 text-sm">{status}</p>}
    </form>
  );
}
