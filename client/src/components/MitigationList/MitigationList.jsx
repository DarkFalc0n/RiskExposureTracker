

import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import './MitigationList.css';
import axios from 'axios';

const MitigationList = () => {
  const [mitigations, setMitigations] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const riskIdOptions = [101, 102, 103, 104, 105];
  const [selectedRiskId, setSelectedRiskId] = useState(riskIdOptions[0]);

  useEffect(() => {
    if (!selectedRiskId) {
      setMitigations([]);
      return;
    }
    const getMitigationData = async () => {
      setLoading(true);
      setError(null);
      try {
        const backendUrl = `https://localhost:7092/api/Mitigations/${selectedRiskId}`;
        const response = await axios.get(backendUrl);
        setMitigations(response.data);
      } catch (err) {
        if (err.response && err.response.status === 404) {
          setMitigations([]);
        } else {
          console.error('Failed to fetch mitigation data:', err);
          setError(`Could not retrieve data for Risk ID ${selectedRiskId}.`);
        }
      } finally {
        setLoading(false);
      }
    };
    getMitigationData();
  }, [selectedRiskId]);

  return (
    <div className="mitigation-container">
      <h2>Mitigation Tracking</h2>
      <p>Select a risk to view its associated mitigation actions.</p>
      <div className="risk-selector-container" style={{ marginBottom: '1.5rem', textAlign: 'left' }}>
        <label htmlFor="risk-select" style={{ marginRight: '10px', fontWeight: 'bold' }}>Select a Risk ID:</label>
        <select
          id="risk-select"
          value={selectedRiskId}
          onChange={(e) => setSelectedRiskId(e.target.value)}
          style={{ padding: '8px', borderRadius: '4px', border: '1px solid #ccc' }}
        >
          {riskIdOptions.map((id) => (
            <option key={id} value={id}>
              {id}
            </option>
          ))}
        </select>
      </div>
      {error && <div className="error-message">{error}</div>}
      {loading && <div className="loading-message">Loading mitigation data...</div>}
      <div className="table-wrapper">
        <table>
          <thead>
            <tr>
              <th>Mitigation ID</th>
              <th>Action</th>
              <th>Owner</th>
              <th>Deadline</th>
              <th>Status</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {!loading && mitigations.length > 0 ? (
              mitigations.map((item) => (
                <MitigationRow key={item.mitigationId} item={item} />
              ))
            ) : (
              <tr>
                <td colSpan="6" className="no-data-cell">
                  {loading ? "" : "No mitigation actions found for this risk."}
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
};

const MitigationRow = ({ item }) => {
  const [expanded, setExpanded] = useState(false);
  const maxChars = 100;

  const displayText =
    item.action.length > maxChars && !expanded
      ? item.action.substring(0, maxChars) + "..."
      : item.action;

  return (
    <tr>
      <td style={{ textAlign: 'center' }}>{item.mitigationId}</td>
      <td>
        {displayText}
        {item.action.length > maxChars && (
          <button
            onClick={() => setExpanded(!expanded)}
            className="see-more-btn"
          >
            {expanded ? "See less" : "See more"}
          </button>
        )}
      </td>
      <td>{item.owner}</td>
      <td style={{ textAlign: 'center' }}>{new Date(item.deadline).toLocaleDateString()}</td>
      <td style={{ textAlign: 'center' }}>
        <span className={`status-pill status-${item.status.toLowerCase()}`}>
          {item.status}
        </span>
      </td>
      <td>
        <Link to={`/details/${item.mitigationId}`} className="action-btn details-btn">
          Details
        </Link>
        <button className="action-btn update-btn">Update</button>
      </td>
    </tr>
  );
};

export default MitigationList;

