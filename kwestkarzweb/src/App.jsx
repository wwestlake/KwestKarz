import { useState, useEffect } from 'react';
import LoginPage from './LoginPage';
import MainPage from './MainPage';
import { jwtDecode } from 'jwt-decode';

function App() {
    const [token, setToken] = useState(null);
    const [userRole, setUserRole] = useState(null);

    useEffect(() => {
        const savedToken = localStorage.getItem('token');
        if (savedToken) {
            setToken(savedToken);
            decodeToken(savedToken);
        }
    }, []);

    const handleLoginSuccess = (newToken) => {
        localStorage.setItem('token', newToken);
        setToken(newToken);
        decodeToken(newToken);
    };

    const decodeToken = (token) => {
        try {
            const decoded = jwtDecode(token);
            const role = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
            setUserRole(role);
        } catch (e) {
            console.error("Invalid token", e);
            setUserRole(null);
        }
    };

    const handleLogout = () => {
        localStorage.removeItem('token');
        setToken(null);
        setUserRole(null);
    };

    return (
        <>
            {token ? (
                <MainPage token={token} userRole={userRole} onLogout={handleLogout} />
            ) : (
                <LoginPage onLoginSuccess={handleLoginSuccess} />
            )}
        </>
    );
}

export default App;
