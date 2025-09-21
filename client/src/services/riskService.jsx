import axios from "axios";

const API_URL = "https://localhost:7260/api/v1/risks"; // make sure backend matches this

// Create axios instance
const axiosObj = axios.create();

// Request logging
axiosObj.interceptors.request.use(
  (config) => {
    console.log("Sending request:", config.method?.toUpperCase(), config.url, config.data);
    return config;
  },
  (error) => Promise.reject(error)
);

// Response logging
axiosObj.interceptors.response.use(
  (response) => response,
  (error) => {
    console.error(
      "API Error:",
      error.response?.status,
      error.response?.data || error.message
    );
    return Promise.reject(error);
  }
);

class RiskService {
  async getByOrg(orgId) {
    return axiosObj.get(`${API_URL}/org/${orgId}`);
  }

  // ✅ Get a single risk by riskId
  async getById(id){
    return axiosObj.get(`${API_URL}/riskid/${id}`);
  } 

  async addRisk(risk) {
    return axiosObj.post(API_URL, risk); // sends JSON by default
  }

  async update(id, risk) {
    return axiosObj.put(`${API_URL}/${id}`, risk);
  }

  
}

const riskService = new RiskService();
export default riskService;
