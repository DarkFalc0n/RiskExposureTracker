import axios from "axios";

const API_URL = "https://localhost:7126/api/reports"; 

const getReports = (orgId, from, to) => {
  let url = `${API_URL}/${orgId}`;
  return axios.get(url);
};

const createReport = (report) => {
  return axios.post(API_URL, report);
};

export default {
  getReports,
  createReport,
};
