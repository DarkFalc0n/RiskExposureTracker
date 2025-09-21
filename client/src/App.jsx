// import { useState } from 'react'
// import './App.css'

// function App() {
//   const [count, setCount] = useState(0)

//   return (
//     <>
//       <p className="read-the-docs">
//         Click on the Vite and React logos to learn more
//       </p>
//     </>
//   )
// }

// export default App
// App.jsx
import React from 'react';
import MitigationList from './components/MitigationList/MitigationList.jsx'; // Corrected import path
import './App.css';

function App() {
  // Capture the time when the component first renders (page refresh/load)
  const lastRefreshed = new Date().toLocaleString();

  return (
    <div className="app-container">
      <header className="app-header">
        <h1>RiskLens</h1>
      </header>
      <main>
        <MitigationList />
      </main>
      <footer className="app-footer">
        <p><h3>&copy; Prayas@RiskLens.com • Last updated: {lastRefreshed}</h3></p>
        <p></p>
      </footer>
    </div>
  );
}

export default App;
