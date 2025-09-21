import { useParams, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import riskService from "../services/riskService";
import EditRiskForm from "./EditRiskForm";

function EditRiskPage() {
  const { orgId, riskId } = useParams();
  const numericRiskId = Number(riskId); 
  const numericOrgId = Number(orgId);   
  const navigate = useNavigate();
  const [editingRisk, setEditingRisk] = useState(null);

  // Fetch risk details by ID when page loads
  useEffect(() => {
    async function fetchRisk() {
      try {
        const numericRiskId = Number(riskId); 
        const res = await riskService.getById(numericRiskId);
        setEditingRisk(res.data);
      } catch (err) {
        console.error("Error fetching risk:", err);
      }
    }
    fetchRisk();
  }, [riskId]);

  const handleUpdate = (updatedRisk) => {
    console.log("Updated:", updatedRisk);
    // After update, go back to org's risks list
    navigate(`/org/${orgId}`);
  };

  return editingRisk ? (
    <EditRiskForm
      orgId={orgId}
      editingRisk={editingRisk}
      onUpdate={handleUpdate}
      clearEditing={() => navigate(`/org/${orgId}`)}
    />
  ) : (
    <p className="text-center mt-8">Loading risk...</p>
  );
}

export default EditRiskPage;
