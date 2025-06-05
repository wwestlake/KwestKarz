import { useEffect, useState } from "react";
import {
  getUsers,
  getUserById,
  createUser,
  updateUser,
  disableUser,
  enableUser,
} from "../services/userService";

export default function UserManagement() {
  const [users, setUsers] = useState([]);
  const [filter, setFilter] = useState("");
  const [selectedUser, setSelectedUser] = useState(null);
  const [error, setError] = useState("");

  useEffect(() => {
    loadUsers();
  }, []);

  async function loadUsers() {
    try {
      const data = await getUsers();
      setUsers(data);
    } catch (err) {
      setError(err.message);
    }
  }

  function handleSelect(user) {
    setSelectedUser(user);
  }

  const filteredUsers = users.filter((u) =>
    u.email.toLowerCase().includes(filter.toLowerCase())
  );

  return (
    <div className="flex gap-4 p-4">
      <div className="w-1/3">
        <input
          type="text"
          placeholder="Filter by email..."
          value={filter}
          onChange={(e) => setFilter(e.target.value)}
          className="w-full mb-4 p-2 border rounded"
        />
        <ul className="border rounded divide-y max-h-[70vh] overflow-auto">
          {filteredUsers.map((user) => (
            <li
              key={user.id}
              className="p-2 hover:bg-accent hover:text-white cursor-pointer"
              onClick={() => handleSelect(user)}
            >
              {user.email}
            </li>
          ))}
        </ul>
      </div>
      <div className="w-2/3 border rounded p-4">
        {selectedUser ? (
          <div>
            <h2 className="text-xl font-semibold mb-4">{selectedUser.username}</h2>
            <p>Email: {selectedUser.email}</p>
            <p>Status: {selectedUser.isActive ? "Active" : "Disabled"}</p>
            <div className="mt-4 flex gap-2">
              {selectedUser.isActive ? (
                <button
                  onClick={() => {
                    disableUser(selectedUser.id).then(loadUsers);
                  }}
                  className="bg-red-500 text-white px-4 py-2 rounded"
                >
                  Disable
                </button>
              ) : (
                <button
                  onClick={() => {
                    enableUser(selectedUser.id).then(loadUsers);
                  }}
                  className="bg-green-500 text-white px-4 py-2 rounded"
                >
                  Enable
                </button>
              )}
            </div>
          </div>
        ) : (
          <p>Select a user to view details.</p>
        )}
      </div>
    </div>
  );
}
