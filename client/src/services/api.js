
import axios from 'axios';

const API_URL = 'https://localhost:7262/api/Organizations';

export const getAllOrganizations = () => {
  return axios.get(API_URL);
};

export const getOrganizationById = (orgId) => {
  return axios.get(`${API_URL}/${orgId}`);
};

export const updateOrganization = (orgId, updatedOrg) => {
  return axios.put(`${API_URL}/${orgId}`, updatedOrg);
};
