import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getOrganizationById } from '../services/api';
import './OrganizationDetails.css'; 

function OrganizationDetails() {
  const { orgId } = useParams();
  const navigate = useNavigate();
  const [organization, setOrganization] = useState(null);

  useEffect(() => {
    fetchOrganizationDetails();
  }, [orgId]);

  const fetchOrganizationDetails = async () => {
    try {
      const response = await getOrganizationById(orgId);
      setOrganization(response.data);
    } catch (err) {
      console.error('Error fetching organization details:', err);
    }
  };

  const handleEditClick = () => {
    navigate(`/organization/edit/${orgId}`);
  };

  const handleListClick = () => {
    navigate(`/`);
  };

  if (!organization) return <p>Loading...</p>;

  return (
    
    <div className="organization-details">  {/* Ensure this class is applied */}
      <h1>Organization Details</h1>
      <div className="card">
        <div className="card-body">
          <h5 className="card-title">{organization.name}</h5>
          <p className="card-text">Sector: {organization.sector}</p>
          <p className="card-text">Region: {organization.region}</p>
          <p className="card-text">Contact: {organization.contact}</p>
          <p className="card-text">Email: {organization.email}</p>

          {/* Button container with side-by-side buttons */}
          <div className="button-container">
            <button className="btn btn-primary" onClick={handleEditClick}>Edit</button>
            <button className="btn btn-primary" onClick={handleListClick}>Back to list</button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default OrganizationDetails;
