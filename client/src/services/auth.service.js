import api from "@/config/axios";
import { API_URLS } from "@/constants/apiUrls";

export const login = async (email, password) => {
    const response = await api.post(API_URLS.LOGIN, { email, password });
    localStorage.setItem("token", response.data.token);
    return response.data;
}

export const register = async (name, sector, region, contact, email, password) => {
    const response = await api.post(API_URLS.REGISTER, { name, sector, region, contact, email, password });
    localStorage.setItem("token", response.data.token);
    return response.data;
}

export const logout = async () => {
    localStorage.removeItem("token");
    window.location.href = "/auth/login";
}

export const getUser = async () => {
    const response = await api.get(API_URLS.ME);
    return response.data;
}