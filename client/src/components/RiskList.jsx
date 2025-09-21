import React from "react";
import RiskForm from "./RiskForm";

export default function RiskList({ risks, onUpdate }) {
  return (
    <div className="overflow-x-auto mt-6">
      <table className="min-w-full border border-gray-300 rounded-lg shadow-sm">
        <thead className="bg-indigo-600 text-white">
          <tr>
            <th className="px-4 py-2 text-left">Risk ID</th>
            <th className="px-4 py-2 text-left">Org ID</th>
            <th className="px-4 py-2 text-left">Category</th>
            <th className="px-4 py-2 text-left">Description</th>
            <th className="px-4 py-2 text-left">Exposure</th>
            <th className="px-4 py-2 text-left">Status</th>
            <th className="px-4 py-2 text-left">Created At</th>
            <th className="px-4 py-2 text-center">Actions</th>
          </tr>
        </thead>
        <tbody>
          {risks.length > 0 ? (
            risks.map((risk, index) => (
              <tr
                key={risk.riskId}
                className={`${
                  index % 2 === 0 ? "bg-gray-50" : "bg-white"
                } hover:bg-gray-100 transition`}
              >
                <td className="px-4 py-2 font-medium text-gray-700">{risk.riskId}</td>

                <td className="px-4 py-2 font-medium text-gray-700">{risk.orgId}</td>

                <td className="px-4 py-2 text-indigo-600 font-semibold">
                  {risk.category}
                </td>

                <td className="px-4 py-2 font-medium text-gray-700">{risk.description}</td>

                <td className="px-4 py-2 font-medium text-gray-700">
                  {risk.exposure}
                </td>

                <td className="px-4 py-2">
                  <span
                    className={`px-2 py-1 rounded-full text-xs font-semibold ${
                      risk.status === "Open"
                        ? "bg-blue-100 text-blue-800"
                        : risk.status === "Mitigated"
                        ? "bg-yellow-100 text-yellow-800"
                        : "bg-green-100 text-green-800"
                    }`}
                  >
                    {risk.status}
                  </span>
                </td>

                <td className="px-4 py-2 text-gray-500 text-sm">
                  {new Date(risk.createdAt).toLocaleString()}
                </td>
                
                <td className="px-4 py-2 text-center space-x-2">
                  <button
                    onClick={() => onUpdate(risk)}
                    className="bg-indigo-500 text-white px-3 py-1 rounded-md hover:bg-indigo-600 transition"
                  >
                    Edit
                  </button>
                  
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td
                colSpan="8"
                className="px-4 py-4 text-center text-gray-400 italic"
              >
                No risks available
              </td>
            </tr>
          )}
        </tbody>
      </table>

      <div>
        
      </div>
    </div>
    
  );
}
