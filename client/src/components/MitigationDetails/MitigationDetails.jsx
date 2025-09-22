import React, { useState, useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import axios from 'axios';
import './MitigationDetails.css';

const MitigationDetails = () => {
  const { mitigationId } = useParams();
  const [mitigation, setMitigation] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchMitigationDetails = async () => {
      setLoading(true);
      setError(null);
      try {
        const backendUrl = `https://localhost:7092/api/Mitigations/details/${mitigationId}`;
        const response = await axios.get(backendUrl);
        setMitigation(response.data);
      } catch (err) {
        setError(`Failed to fetch details for mitigation ID ${mitigationId}.`);
      } finally {
        setLoading(false);
      }
    };
    fetchMitigationDetails();
  }, [mitigationId]);

  if (loading) return <div className="loading-message">Loading details...</div>;
  if (error) return <div className="error-message">{error}</div>;
  if (!mitigation) return <div className="loading-message">No data found for this mitigation.</div>;

  return (
    <div className="details-container">
      <div className="details-card">
        <div className="details-header">
          <h2>Mitigation Detail</h2>
          <p>Mitigation ID: {mitigation.mitigationId}</p>
        </div>
        <div className="details-body">
          <div className="detail-item">
            <strong>Action Description</strong>
            <p>{mitigation.action}</p>
          </div>
          <div className="detail-item">
            <strong>Associated Risk ID</strong>
            <p>{mitigation.riskId}</p>
          </div>
          <div className="detail-item">
            <strong>Owner</strong>
            <p>{mitigation.owner}</p>
          </div>
          <div className="detail-item">
            <strong>Deadline</strong>
            <p>{new Date(mitigation.deadline).toLocaleDateString()}</p>
          </div>
          <div className="detail-item">
            <strong>Status</strong>
            <p>
                <span className={`status-pill status-${mitigation.status.toLowerCase()}`}>
                    {mitigation.status}
                </span>
            </p>
          </div>
        </div>
        <Link to="/" className="back-link">
          &larr; Back to List
        </Link>
      </div>
    </div>
  );
};

export default MitigationDetails;

