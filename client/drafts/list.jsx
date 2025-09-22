
import React, { useState, useEffect } from 'react';
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

  // Calculate stats
  const totalActions = mitigations.length;
  const completedActions = mitigations.filter(m => m.status?.toLowerCase() === 'completed').length;
  const openActions = mitigations.filter(m => m.status?.toLowerCase() === 'open').length;

  return (
    <div className="mitigation-page">
      {/* Animated Background */}
      <div className="animated-bg"></div>
      <div className="animated-orb orb-1"></div>
      <div className="animated-orb orb-2"></div>
      <div className="animated-orb orb-3"></div>
      
      <div className="mitigation-container">
        {/* Header Section */}
        <div className="mitigation-header">
          <div className="header-content">
            <div className="title-section">
              <div className="title-icon">
                <svg width="36" height="36" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                  <path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z"/>
                </svg>
              </div>
              <div>
                <h1 className="main-title">Mitigation Tracking</h1>
                <p className="main-subtitle">Monitor and manage risk mitigation actions</p>
              </div>
            </div>
            
            {/* Stats Cards */}
            <div className="stats-grid">
              <div className="stat-card stat-total">
                <div className="stat-icon">
                  <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                    <polyline points="22 12 18 12 15 21 9 3 6 12 2 12"/>
                  </svg>
                </div>
                <div className="stat-content">
                  <div className="stat-number">{totalActions}</div>
                  <div className="stat-label">Total Actions</div>
                </div>
              </div>
              
              <div className="stat-card stat-completed">
                <div className="stat-icon">
                  <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                    <path d="M22 11.08V12a10 10 0 1 1-5.93-9.14"/>
                    <polyline points="22 4 12 14.01 9 11.01"/>
                  </svg>
                </div>
                <div className="stat-content">
                  <div className="stat-number">{completedActions}</div>
                  <div className="stat-label">Completed</div>
                </div>
              </div>
              
              <div className="stat-card stat-open">
                <div className="stat-icon">
                  <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                    <circle cx="12" cy="12" r="10"/>
                    <polyline points="12 6 12 12 16 14"/>
                  </svg>
                </div>
                <div className="stat-content">
                  <div className="stat-number">{openActions}</div>
                  <div className="stat-label">In Progress</div>
                </div>
              </div>
            </div>
          </div>
        </div>

        {/* Risk Selector Card */}
        <div className="selector-card">
          <div className="selector-icon">
            <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
              <circle cx="12" cy="12" r="10"/>
              <circle cx="12" cy="12" r="6"/>
              <circle cx="12" cy="12" r="2"/>
            </svg>
          </div>
          <label htmlFor="risk-select" className="selector-label">Select Risk ID:</label>
          <select
            id="risk-select"
            value={selectedRiskId}
            onChange={(e) => setSelectedRiskId(e.target.value)}
            className="risk-select"
          >
            {riskIdOptions.map((id) => (
              <option key={id} value={id}>
                Risk #{id}
              </option>
            ))}
          </select>
        </div>

        {/* Error Message */}
        {error && (
          <div className="error-alert">
            <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
              <circle cx="12" cy="12" r="10"/>
              <line x1="12" y1="8" x2="12" y2="12"/>
              <line x1="12" y1="16" x2="12.01" y2="16"/>
            </svg>
            <span>{error}</span>
          </div>
        )}

        {/* Loading State */}
        {loading && (
          <div className="loading-container">
            <div className="pulse-loader">
              <div className="pulse-ring"></div>
              <div className="pulse-ring"></div>
              <div className="pulse-ring"></div>
            </div>
            <p className="loading-text">Loading mitigation data...</p>
          </div>
        )}

        {/* Data Table */}
        {!loading && (
          <div className="table-card">
            <table className="mitigation-table">
              <thead>
                <tr>
                  <th>
                    <div className="th-content">
                      <span className="th-number">#</span>
                      ID
                    </div>
                  </th>
                  <th className="th-action">
                    <div className="th-content">
                      <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                        <polyline points="23 6 13.5 15.5 8.5 10.5 1 18"/>
                      </svg>
                      Action
                    </div>
                  </th>
                  <th>
                    <div className="th-content">
                      <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                        <path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"/>
                        <circle cx="12" cy="7" r="4"/>
                      </svg>
                      Owner
                    </div>
                  </th>
                  <th>
                    <div className="th-content">
                      <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                        <rect x="3" y="4" width="18" height="18" rx="2" ry="2"/>
                        <line x1="16" y1="2" x2="16" y2="6"/>
                        <line x1="8" y1="2" x2="8" y2="6"/>
                        <line x1="3" y1="10" x2="21" y2="10"/>
                      </svg>
                      Deadline
                    </div>
                  </th>
                  <th>Status</th>
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {mitigations.length > 0 ? (
                  mitigations.map((item, index) => (
                    <MitigationRow 
                      key={item.mitigationId} 
                      item={item}
                      index={index}
                    />
                  ))
                ) : (
                  <tr>
                    <td colSpan="6" className="no-data-cell">
                      <div className="no-data-content">
                        <svg width="48" height="48" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.5" opacity="0.3">
                          <path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z"/>
                        </svg>
                        <p>No mitigation actions found for Risk #{selectedRiskId}</p>
                      </div>
                    </td>
                  </tr>
                )}
              </tbody>
            </table>
          </div>
        )}
      </div>
    </div>
  );
};

// Row Component with animations and improved styling
const MitigationRow = ({ item, index }) => {
  const [expanded, setExpanded] = useState(false);
  const maxChars = 100;

  const displayText =
    item.action.length > maxChars && !expanded
      ? item.action.substring(0, maxChars) + "..."
      : item.action;

  return (
    <tr className="table-row" style={{ animationDelay: `${index * 0.05}s` }}>
      <td>
        <div className="id-badge">
          {item.mitigationId}
        </div>
      </td>
      <td className="action-cell">
        <div className="action-content">
          {displayText}
          {item.action.length > maxChars && (
            <button
              onClick={() => setExpanded(!expanded)}
              className="expand-btn"
            >
              {expanded ? (
                <>
                  <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                    <polyline points="18 15 12 9 6 15"/>
                  </svg>
                  See less
                </>
              ) : (
                <>
                  <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                    <polyline points="6 9 12 15 18 9"/>
                  </svg>
                  See more
                </>
              )}
            </button>
          )}
        </div>
      </td>
      <td>
        <div className="owner-cell">
          <div className="owner-avatar">
            {item.owner.charAt(0)}
          </div>
          <span className="owner-name">{item.owner}</span>
        </div>
      </td>
      <td>
        <div className="deadline-cell">
          <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" opacity="0.7">
            <rect x="3" y="4" width="18" height="18" rx="2" ry="2"/>
            <line x1="16" y1="2" x2="16" y2="6"/>
            <line x1="8" y1="2" x2="8" y2="6"/>
            <line x1="3" y1="10" x2="21" y2="10"/>
          </svg>
          {new Date(item.deadline).toLocaleDateString('en-US', {
            month: 'short',
            day: 'numeric',
            year: 'numeric'
          })}
        </div>
      </td>
      <td>
        <span className={`status-badge status-${item.status?.toLowerCase()}`}>
          {item.status?.toLowerCase() === 'completed' ? (
            <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
              <path d="M22 11.08V12a10 10 0 1 1-5.93-9.14"/>
              <polyline points="22 4 12 14.01 9 11.01"/>
            </svg>
          ) : (
            <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
              <circle cx="12" cy="12" r="10"/>
              <polyline points="12 6 12 12 16 14"/>
            </svg>
          )}
          {item.status}
        </span>
      </td>
      <td>

        
        <div className="action-buttons">


          <button className="btn-primary">
            <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
              <circle cx="12" cy="12" r="10"/>
              <line x1="12" y1="16" x2="12" y2="12"/>
              <line x1="12" y1="8" x2="12.01" y2="8"/>
            </svg>
            <span>Details</span>
          </button>



          <button className="btn-secondary">
            <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
              <path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"/>
              <path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"/>
            </svg>
            <span>Update</span>
          </button>
        </div>
      </td>
    </tr>
  );
};

export default MitigationList;
