import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Layout from "./components/Layout";

import Home from "./pages/Home";
import Vehicles from "./pages/Vehicles";
import Turo from "./pages/Turo";
import Help from "./pages/Help";
import Contact from "./pages/Contact";
import Login from "./pages/Login";
import DashboardLayout from "./components/DashboardLayout";
import { Navigate } from "react-router-dom";
import { clearToken } from "./services/sessionService";
import UserManagement from "./pages/UserManagement";
import InviteUser from "./pages/InviteUser";
import CompleteAccountSetup from "./pages/CompleteAccountSetup";
import ChangePassword from "./pages/ChangePassword";

function App() {
  return (
    <Router>
      <Layout>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/vehicles" element={<Vehicles />} />
          <Route path="/turo" element={<Turo />} />
          <Route path="/help" element={<Help />} />
          <Route path="/contact" element={<Contact />} />
          <Route path="/login" element={<Login />} />
          <Route path="/dashboard" element={<DashboardLayout />} />
          <Route path="/dashboard/users" element={<UserManagement />} />
          <Route path="/dashboard/users/invite" element={<InviteUser />} />
          {/* Add more dashboard routes as needed */}
          <Route path="/setup-account" element={<CompleteAccountSetup />} />
          <Route path="/change-password" element={<ChangePassword />} />
          <Route path="/logout" element={
                (() => {
                  clearToken();
                  return <Navigate to="/login" replace />;
                })()
          } />
        </Routes>
      </Layout>
    </Router>
  );
}

export default App;
