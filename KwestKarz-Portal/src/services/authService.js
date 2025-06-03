import config from '../config';

export async function login(usernameOrEmail, password) {
  const res = await fetch(`${config.apiBaseUrl}/api/Auth/login`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ usernameOrEmail, password }),
  });

  if (!res.ok) {
    throw new Error('Login failed');
  }

  const data = await res.json();
  return data.token;
}
