// App.jsx
import React from 'react';
import MitigationList from './components/MitigationList/MitigationList.jsx'; // Corrected import path
import './App.css';
import { Routes, Route } from 'react-router-dom';
import MitigationDetails from './components/MitigationDetails/MitigationDetails.jsx';

function App() {
  // Capture the time when the component first renders (page refresh/load)
  const lastRefreshed = new Date().toLocaleString();

  return (
    <div className="app-container">
      <header className="app-header">
        <h1>RiskLens</h1>
      </header>
      <main>

       {/* <MitigationList /> */}
       
       
        <Routes>
          <Route path="/" element={<MitigationList />} />
          <Route path="/details/:mitigationId" element={<MitigationDetails />} />
        </Routes>

        
      </main>
      <footer className="app-footer">
        <p><h3>&copy; Prayas@RiskLens.com • Last updated: {lastRefreshed}</h3></p>
        <p></p>
      </footer>
    </div>
  );
}

export default App;
