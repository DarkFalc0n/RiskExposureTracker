import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

function OrgSelector() {
  const [orgId, setOrgId] = useState("");
  const navigate = useNavigate();

  const handleSubmit = (e) => {
    e.preventDefault();
    if (orgId) navigate(`/org/${Number(orgId)}`);
    ;
  };

  return (
    <div className="max-w-md mx-auto mb-10 bg-white shadow-lg rounded-3xl p-6 border border-gray-200">
      <form onSubmit={handleSubmit}>
        <label className="block text-gray-700 font-semibold mb-3">
          Select Organization ID:
        </label>
        <input
          type="number"
          value={orgId}
          onChange={(e) => setOrgId(e.target.value)}
          placeholder="Enter Org ID"
          className="w-full border border-gray-300 rounded-xl px-4 py-2 
                     focus:outline-none focus:ring-2 focus:ring-indigo-500 transition
                     text-black"
        />
        <button
          type="submit"
          className="mt-4 w-full bg-indigo-600 text-white py-2 rounded-xl hover:bg-indigo-700 transition"
        >
          Go to Dashboard
        </button>
      </form>
    </div>
  );
}

export default OrgSelector;
