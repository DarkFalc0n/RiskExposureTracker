import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getAllOrganizations } from '../services/api';
import './Organizations.css';

function Organizations() {
  const [organizations, setOrganizations] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    fetchOrganizations();
  }, []);

  const fetchOrganizations = async () => {
    try {
      const response = await getAllOrganizations();
      setOrganizations(response.data);
    } catch (err) {
      console.error('Error fetching organizations:', err);
    }
  };

  const handleEditClick = (orgId) => {
    navigate(`/organization/edit/${orgId}`);
  };

  const handleDetailsClick = (orgId) => {
    navigate(`/organization/${orgId}`);
  };

  const resultsArray = organizations.map((org) => (
    <tr key={org.orgId}>
      <td>{org.orgId}</td>
      <td>{org.name}</td>
      <td>{org.sector}</td>
      <td>{org.region}</td>
      <td>{org.contact}</td>
      <td>{org.email}</td>
      <td>
        <button style={{ backgroundColor: 'blue', color: 'white' }} onClick={() => handleDetailsClick(org.orgId)}>Details</button>{' '}
        <button style={{ backgroundColor: 'green', color: 'white' }}onClick={() => handleEditClick(org.orgId)}>Edit</button>
      </td>
    </tr>
  ));

  return (
    <div className="flex justify-center">
      {/* Fixed header */}
      <div className="fixed-header">
        <h1 className="text-center text-2xl font-bold mb-4">Organizations</h1>
      </div>

      {/* Table content */}
      <div className="flex-container">
        <div className="table-container">
          <table>
            <thead>
              <tr>
                <th>Organization ID</th>
                <th>Organization Name</th>
                <th>Sector</th>
                <th>Region</th>
                <th>Contact Person</th>
                <th>Email Id</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>{resultsArray}</tbody>
          </table>
        </div>
      </div>
    </div>
  );
}

export default Organizations;
