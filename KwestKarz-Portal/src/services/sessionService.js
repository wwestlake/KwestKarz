const TOKEN_KEY = 'kwestkarz_jwt';

export function saveToken(token) {
  localStorage.setItem(TOKEN_KEY, token);
}

export function getToken() {
  return localStorage.getItem(TOKEN_KEY);
}

export function clearToken() {
  localStorage.removeItem(TOKEN_KEY);
}

export function isLoggedIn() {
  return !!getToken();
}

export function getClaims() {
  const token = getToken();
  if (!token) return null;

  try {
    const [, payload] = token.split('.');
    const decoded = JSON.parse(atob(payload));
    return decoded;
  } catch (err) {
    return null;
  }
}

export function getRoles() {
  const claims = getClaims();
  if (!claims) return [];
  
  // Your roles claim path may vary depending on how it's issued
  const roleClaim = Object.entries(claims).find(([key]) => key.toLowerCase().includes('/role'));
  return roleClaim ? roleClaim[1].split(',') : [];
}
