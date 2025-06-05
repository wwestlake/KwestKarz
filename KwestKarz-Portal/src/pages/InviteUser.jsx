import { useState } from "react";
import { inviteUser } from "../services/userService";

export default function InviteUser() {
  const [email, setEmail] = useState("");
  const [status, setStatus] = useState("");

  async function handleInvite(e) {
    e.preventDefault();
    setStatus("");

    try {
      await inviteUser(email);
      setStatus("Invite sent!");
      setEmail("");
    } catch (err) {
      setStatus("Error sending invite.");
    }
  }

  return (
    <form onSubmit={handleInvite} className="max-w-md mx-auto p-4">
      <h2 className="text-xl font-bold mb-4">Send User Invite</h2>
      <input
        type="email"
        required
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        placeholder="user@example.com"
        className="w-full mb-2 p-2 border rounded bg-white text-black"
      />
      <button type="submit" className="bg-accent text-white px-4 py-2 rounded">
        Send Invite
      </button>
      {status && <p className="mt-2 text-sm">{status}</p>}
    </form>
  );
}
