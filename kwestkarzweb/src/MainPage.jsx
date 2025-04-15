import React from 'react';

function MainPage({ onLogout, userRole }) {
    return (
        <div className="vh-100 d-flex justify-content-center align-items-center bg-light">
            <div className="w-100 mx-auto text-center d-flex flex-column align-items-center" style={{ maxWidth: '400px' }}>

                <img
                    src="/KwestKarzLogo.webp"
                    alt="KwestKarz Logo"
                    className="mb-4 img-fluid"
                    style={{ maxWidth: '200px' }}
                />

                <div className="d-grid gap-3 w-100">
                    {(userRole === 'Owner' || userRole === 'Admin') && (
                        <button className="btn btn-primary btn-lg">Fleet</button>
                    )}

                    {(userRole === 'Owner' || userRole === 'Admin' || userRole === 'Employee') && (
                        <button className="btn btn-secondary btn-lg">Maintenance</button>
                    )}

                    {(userRole === 'Owner' || userRole === 'Admin') && (
                        <>
                            <button className="btn btn-success btn-lg">Finance</button>
                            <button className="btn btn-warning btn-lg">Users</button>
                        </>
                    )}

                    {userRole === 'Admin' && (
                        <button className="btn btn-dark btn-lg">Administration</button>
                    )}
                </div>

                <div className="mt-4">
                    <a href="#" className="text-decoration-none small">Your Account</a>
                </div>

                <div className="mt-3">
                    <button className="btn btn-outline-danger btn-sm w-100" onClick={onLogout}>Logout</button>
                </div>

            </div>
        </div>
    );
}

export default MainPage;
