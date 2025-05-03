import { useState } from 'react';

function LoginPage({ onLoginSuccess }) {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');

    const handleLogin = async () => {
        setError('');

        try {
            const response = await fetch('/api/Auth/login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ usernameOrEmail: username, password }),
            });

            if (!response.ok) throw new Error('Login failed');

            const data = await response.json();
            onLoginSuccess(data.token);
        } catch (err) {
            setError(err.message || 'Unknown error');
        }
    };

    return (
        <div className="d-flex justify-content-center align-items-center vh-100 bg-light">
            <div className="w-100" style={{ maxWidth: '400px' }}>
                <div className="text-center mb-4">
                    <img
                        src="/KwestKarzLogo.webp"
                        alt="KwestKarz Logo"
                        className="img-fluid"
                        style={{ maxWidth: '200px' }}
                    />
                </div>
                <div className="card shadow">
                    <div className="card-body">
                        <h3 className="card-title text-center mb-4">Login</h3>
                        <div className="mb-3">
                            <input
                                type="text"
                                className="form-control"
                                placeholder="Username or Email"
                                value={username}
                                onChange={(e) => setUsername(e.target.value)}
                            />
                        </div>
                        <div className="mb-3">
                            <input
                                type="password"
                                className="form-control"
                                placeholder="Password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                            />
                        </div>
                        {error && <div className="alert alert-danger">{error}</div>}
                        <div className="d-grid">
                            <button className="btn btn-primary" onClick={handleLogin}>
                                Login
                            </button>
                        </div>
                    </div>
                </div>
                <div className="text-center mt-4 text-muted small">
                    <p>KwestKarz — Manage your fleet with style.</p>
                    <p>Secure, simple, and straight to the point.</p>
                </div>
            </div>
        </div>
    );
}

export default LoginPage;
