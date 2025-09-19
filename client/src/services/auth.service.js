import api from "@/config/axios";

export const login = async (email, password) => {
    const response = await api.post("/auth/login", { email, password });
    localStorage.setItem("token", response.data.token);
    return response.data;
}

export const register = async (firstName, lastName, email, password) => {
    const response = await api.post("/auth/register", { firstName, lastName, email, password });
    localStorage.setItem("token", response.data.token);
    return response.data;
}
