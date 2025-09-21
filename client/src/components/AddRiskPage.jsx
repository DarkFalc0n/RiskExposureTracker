import React from "react";
import { useParams, useNavigate } from "react-router-dom";
import AddRiskForm from "./AddRiskForm";

function AddRiskPage() {
  const { orgId } = useParams();
  const navigate = useNavigate();

  const handleAdd = () => {
    navigate(`/org/${orgId}`);
  };

  return (
    <div className="max-w-3xl mx-auto">
      <AddRiskForm orgId={orgId} onAdd={handleAdd} />
    </div>
  );
}

export default AddRiskPage;
