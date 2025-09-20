import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getOrganizationById, updateOrganization } from '../services/api';
import './OrganizationEdit.css';

function OrganizationEdit() {
  const { orgId } = useParams();
  const navigate = useNavigate();
  
  const [organization, setOrganization] = useState({
    name: '',
    sector: '',
    region: '',
    contact: '',
    email: ''
  });

  const [errorObj, setErrorObj] = useState({
    name: '',
    sector: '',
    region: '',
    contact: '',
    email: ''
  });

  const [result, setResult] = useState('');

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

  const handleListClick = () => {
    navigate(`/`);
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setOrganization((prevOrganization) => ({...prevOrganization, [name]: value}));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    let tempErrorObj = { ...errorObj };

    // Email validation regex
    const validEmailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    tempErrorObj.name = organization.name.length === 0 ? 'Organization name is required' : '';
    tempErrorObj.sector = organization.sector.length === 0 ? 'Sector is required' : '';
    tempErrorObj.region = organization.region.length === 0 ? 'Region is required' : '';
    tempErrorObj.contact = organization.contact.length === 0 ? 'Contact is required' : '';
    tempErrorObj.email = !validEmailRegex.test(organization.email) ? 'Invalid email address' : '';

    setErrorObj(tempErrorObj);

    const hasErrors = Object.values(tempErrorObj).some((error) => error !== '');
    if (hasErrors) {
      setResult('You have entered invalid data. Please enter valid data.');
    } else {
      try {
        await updateOrganization(orgId, organization); 
        setResult('Organization details updated successfully!');
        navigate('/');  
      } catch (err) {
        console.error('Error updating organization:', err);
        setResult('Failed to update organization details.');
      }
    }
  };

  if (!organization) return <p>Loading...</p>;

  return (
    <div className="organization-edit"> {/* Add a unique class here */}
      <h1>Edit Organization</h1>
      <form onSubmit={handleSubmit} noValidate>
        <div>
          <label>Name:</label>
          <input
            type="text"
            name="name"
            value={organization.name}
            onChange={handleChange}
          />
          <span style={{ color: 'red' }}>{errorObj.name}</span>
        </div>

        <div>
          <label>Sector:</label>
          <input
            type="text"
            name="sector"
            value={organization.sector}
            onChange={handleChange}
          />
          <span style={{ color: 'red' }}>{errorObj.sector}</span>
        </div>

        <div>
          <label>Region:</label>
          <input
            type="text"
            name="region"
            value={organization.region}
            onChange={handleChange}
          />
          <span style={{ color: 'red' }}>{errorObj.region}</span>
        </div>

        <div>
          <label>Contact:</label>
          <input
            type="text"
            name="contact"
            value={organization.contact}
            onChange={handleChange}
          />
          <span style={{ color: 'red' }}>{errorObj.contact}</span>
        </div>

        <div>
          <label>Email:</label>
          <input
            type="email"
            name="email"
            value={organization.email}
            onChange={handleChange}
          />
          <span style={{ color: 'red' }}>{errorObj.email}</span>
        </div>

        <button type="submit" style={{ backgroundColor: 'green', color: 'white' }}>Save Changes</button>
      </form>

      <div>
        <button className="btn btn-primary" onClick={handleListClick} style={{ backgroundColor: 'blue', color: 'white' }}>Back to list</button>
        <h3>{result}</h3>
      </div>
    </div>
  );
}

export default OrganizationEdit;
