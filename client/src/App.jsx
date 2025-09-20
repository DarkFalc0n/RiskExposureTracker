
import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Organizations from './Components/Organizations';
import OrganizationDetails from './Components/OrganizationDetails';
import OrganizationEdit from './Components/OrganizationEdit';
import './app.css';


function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Organizations/>} />
        <Route path="/organization/:orgId" element={<OrganizationDetails/>} />
        <Route path="/organization/edit/:orgId" element={<OrganizationEdit/>} />
      </Routes>
    </Router>
  );
}

export default App;
