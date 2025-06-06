import { getToken } from './sessionService';
import config from '../config';

const baseUrl = `${config.apiBaseUrl}/api/UserAccounts`;

function authHeader() {
  const token = getToken();
  return token ? { Authorization: `Bearer ${token}` } : {};
}

export async function getUsers() {
  const res = await fetch(baseUrl, {
    headers: {
      ...authHeader(),
    },
  });
  if (!res.ok) throw new Error('Failed to fetch users');
  return res.json();
}

export async function getUserById(id) {
  const res = await fetch(`${baseUrl}/${id}`, {
    headers: {
      ...authHeader(),
    },
  });
  if (!res.ok) throw new Error('Failed to fetch user');
  return res.json();
}

export async function createUser(data) {
  const res = await fetch(`${config.apiBaseUrl}/api/Auth/create-account`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      ...authHeader(),
    },
    body: JSON.stringify(data),
  });
  if (!res.ok) throw new Error('Failed to create user');
  return res.json();
}

export async function updateUser(id, data) {
  const res = await fetch(`${baseUrl}/${id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
      ...authHeader(),
    },
    body: JSON.stringify(data),
  });
  if (!res.ok) throw new Error('Failed to update user');
}

export async function disableUser(id) {
  const res = await fetch(`${baseUrl}/${id}/disable`, {
    method: 'POST',
    headers: authHeader(),
  });
  if (!res.ok) throw new Error('Failed to disable user');
}

export async function enableUser(id) {
  const res = await fetch(`${baseUrl}/${id}/enable`, {
    method: 'POST',
    headers: authHeader(),
  });
  if (!res.ok) throw new Error('Failed to enable user');
}

export async function inviteUser(email) {
  const res = await fetch(`${baseUrl}/invite`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      ...authHeader(),
    },
    body: JSON.stringify({ email }),
  });
  if (!res.ok) throw new Error('Failed to send invite');
}

export async function changePassword(data) {
    const res = await fetch(`${config.apiBaseUrl}/api/Auth/change-password`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        ...authHeader(),
      },
      body: JSON.stringify(data),
    });
  
    if (!res.ok) throw new Error('Failed to change password');
  }
  
