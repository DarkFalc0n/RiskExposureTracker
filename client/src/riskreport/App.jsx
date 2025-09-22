import React, { useState } from "react";
import ReportList from "./components/ReportList";
import CreateReport from "./components/CreateReport";
import './App.css';

function App() {
  const [orgId, setOrgId] = useState("");
  const [refresh, setRefresh] = useState(false);

  return (
    <div className="min-h-screen bg-gray-50 p-6">
      <h1 className="text-3xl font-bold mb-6 text-black">Risk Reports</h1>

      {/* Org ID input */}
      <div className="mb-6 flex flex-col sm:flex-row items-start sm:items-center gap-4">
        <label className="text-black font-medium">Org ID:</label>
        <input
          type="number"
          value={orgId}
          onChange={(e) => setOrgId(e.target.value)}
          className="p-2 border rounded shadow-sm w-full sm:w-40 text-black"
          placeholder="Enter Org ID"
        />
      </div>

      {/* ReportList */}
      <div className="mb-6">
        <ReportList key={refresh} orgId={orgId} />
      </div>

      {/* CreateReport */}
      <div>
        <CreateReport orgId={orgId} onReportCreated={() => setRefresh(!refresh)} />
      </div>
    </div>
  );
}

export default App;
