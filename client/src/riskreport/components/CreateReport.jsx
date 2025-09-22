import React, { useState } from "react";
import reportService from "../services/ReportService";

function CreateReport({ orgId, onReportCreated }) {
  const [period, setPeriod] = useState("");
  const [creditRisk, setCreditRisk] = useState(0);
  const [marketRisk, setMarketRisk] = useState(0);
  const [operationalRisk, setOperationalRisk] = useState(0);
  const [complianceRisk, setComplianceRisk] = useState(0);
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);

    const payload = {
      orgId, // send orgId in payload
      period,
      metrics: JSON.stringify({
        creditRisk,
        marketRisk,
        operationalRisk,
        complianceRisk,
      }),
    };

    try {
      await reportService.createReport(payload);
      alert("Report created successfully!");

      // reset form
      setPeriod("");
      setCreditRisk(0);
      setMarketRisk(0);
      setOperationalRisk(0);
      setComplianceRisk(0);

      if (onReportCreated) onReportCreated(); // refresh report list
    } catch (err) {
      console.error("Error creating report:", err.response || err);
      alert("Error creating report");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="p-6 bg-white rounded shadow-md w-full max-w-lg text-black">
      <h2 className="text-xl font-semibold mb-4 text-black">Create Report</h2>
      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label className="block text-sm font-medium mb-1 text-black">Period</label>
          <input
            type="text"
            placeholder="e.g., August 2025"
            value={period}
            onChange={(e) => setPeriod(e.target.value)}
            className="w-full p-2 border rounded text-black"
            required
          />
        </div>

        <div className="grid grid-cols-2 gap-4">
          <div>
            <label className="block text-sm font-medium mb-1 text-black">Credit Risk</label>
            <input
              type="number"
              value={creditRisk}
              onChange={(e) => setCreditRisk(Number(e.target.value))}
              className="w-full p-2 border rounded text-black"
              min="0"
              max="100"
              required
            />
          </div>
          <div>
            <label className="block text-sm font-medium mb-1 text-black">Market Risk</label>
            <input
              type="number"
              value={marketRisk}
              onChange={(e) => setMarketRisk(Number(e.target.value))}
              className="w-full p-2 border rounded text-black"
              min="0"
              max="100"
              required
            />
          </div>
          <div>
            <label className="block text-sm font-medium mb-1 text-black">Operational Risk</label>
            <input
              type="number"
              value={operationalRisk}
              onChange={(e) => setOperationalRisk(Number(e.target.value))}
              className="w-full p-2 border rounded text-black"
              min="0"
              max="100"
              required
            />
          </div>
          <div>
            <label className="block text-sm font-medium mb-1 text-black">Compliance Risk</label>
            <input
              type="number"
              value={complianceRisk}
              onChange={(e) => setComplianceRisk(Number(e.target.value))}
              className="w-full p-2 border rounded text-black"
              min="0"
              max="100"
              required
            />
          </div>
        </div>

        <button
          type="submit"
          disabled={loading}
          className="w-full bg-blue-600 text-white py-2 rounded hover:bg-blue-700 transition-colors"
        >
          {loading ? "Creating..." : "Create Report"}
        </button>
      </form>
    </div>
  );
}

export default CreateReport;
