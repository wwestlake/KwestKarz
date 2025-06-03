import { login as apiLogin } from './authService';
import {
  saveToken,
  getClaims,
  getRoles,
  clearToken,
} from './sessionService';

export async function processLogin(username, password) {
  try {
    const token = await apiLogin(username, password);
    saveToken(token);

    const roles = getRoles();
    const claims = getClaims();

    return {
      success: true,
      token,
      roles,
      claims,
    };
  } catch (err) {
    clearToken();
    return {
      success: false,
      error: err.message || 'Login failed',
    };
  }
}
