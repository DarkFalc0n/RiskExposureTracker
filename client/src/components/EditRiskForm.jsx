import React, { useState, useEffect } from "react";
import riskService from "../services/riskService";

function EditRiskForm({ orgId, onUpdate, editingRisk, clearEditing }) {
  const [risk, setRisk] = useState({
    riskId: null,
    category: "",
    description: "",
    exposure: "",
    status: "Open",
  });

  // Populate form when editingRisk changes
  useEffect(() => {
    if (editingRisk) {
      setRisk(editingRisk);
    }
  }, [editingRisk]);

  const handleChange = (e) => {
    setRisk({ ...risk, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      if (risk.riskId) {
        const payload = { ...risk, orgId, exposure: Number(risk.exposure) };
        const res = await riskService.update(risk.riskId, payload);
        onUpdate(res.data);
        clearEditing();
      }
    } catch (err) {
      console.error("Error updating risk:", err.response?.data || err.message);
    }
  };

  return (
    <form
      onSubmit={handleSubmit}
      className="max-w-md mx-auto bg-white shadow-lg rounded-2xl p-6 space-y-4"
    >
      <h2 className="text-xl font-semibold text-gray-800 mb-4">Edit Risk</h2>

        {/* Category */}
        <label className="block text-gray-700 mb-1">Category</label>
        <input
          name="category"
          placeholder="Enter category"
          value={risk.category}
          onChange={handleChange}
          required
          className="w-full border border-gray-300 rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500 text-black"
        />

        {/* Description */}
        <label className="block text-gray-700 mb-1">Description</label>
        <input
          name="description"
          placeholder="Enter description"
          value={risk.description}
          onChange={handleChange}
          required
          className="w-full border border-gray-300 rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500 text-black"
        />

        {/* Exposure */}
        <label className="block text-gray-700 mb-1">Exposure</label>
        <input
          name="exposure"
          type="number"
          placeholder="Enter exposure amount"
          value={risk.exposure}
          onChange={handleChange}
          required
          className="w-full border border-gray-300 rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500 text-black"
        />

        {/* Status */}
        <label className="block text-gray-700 mb-1">Status</label>
        <select
          name="status"
          value={risk.status}
          onChange={handleChange}
          className="w-full border border-gray-300 rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500 text-black"
        >
          <option className="text-black" value="Open">Open</option>
          <option className="text-black" value="Mitigated">Mitigated</option>
          <option className="text-black" value="Closed">Closed</option>
        </select>
      <br/>

      {/* Buttons */}
      <div className="flex justify-between items-center mt-4">
        <button
          type="submit"
          className="bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700 transition"
        >
          Update Risk
        </button>

        <button
          type="button"
          onClick={clearEditing}
          className="bg-gray-400 text-white px-4 py-2 rounded-lg hover:bg-gray-500 transition"
        >
          Cancel
        </button>
      </div>
      <br/>
    </form>
  );
}

export default EditRiskForm;
