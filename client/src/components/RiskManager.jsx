import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import riskService from "../services/riskService";
import RiskList from "./RiskList";

function RiskManager() {
  const { orgId } = useParams();
  const [risks, setRisks] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchRisks = async () => {
      try {
        const numericOrgId = Number(orgId); 
        const res = await riskService.getByOrg(numericOrgId);
        setRisks(res.data);
      } catch (err) {
        console.error("Failed to fetch risks:", err);
      }
    };
    fetchRisks();
  }, [orgId]);

  return (
    <div className="max-w-6xl mx-auto space-y-8">
      
      <div className="flex justify-between items-center mb-4">
        <h2 className="text-2xl font-bold text-gray-800">Risk List</h2>
        <button
          onClick={() => navigate(`/org/${orgId}/add`)}
          className="bg-indigo-600 text-white px-4 py-2 rounded-lg hover:bg-indigo-700 transition"
        >
          + Add Risk
        </button>
        
      </div>

      <div className="bg-white shadow-lg rounded-3xl p-6 border border-gray-200">
        <RiskList
          risks={risks}
          onUpdate={(risk) => navigate(`/org/${orgId}/edit/${risk.riskId}`)}
        />
      </div>
      <div>
      <button
            onClick={() => navigate(`/`)} // <-- go back to homepage
            className="bg-gray-400 text-white px-4 py-2 rounded-lg hover:bg-gray-500 transition ml-auto">Back to Homepage</button>
      </div>
    </div>
  );
}

export default RiskManager;
