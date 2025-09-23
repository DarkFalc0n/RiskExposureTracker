import api from '@/config/axios'
import { API_URLS } from '@/constants/apiUrls'

export async function createRisk(payload) {
    const response = await api.post(API_URLS.ADD_RISK, payload)
    return response.data
}

export async function updateRisk(id, payload) {
    const url = API_URLS.UPDATE_RISK.replace('{id}', String(id))
    const response = await api.put(url, payload)
    return response.data
}

export async function getRisksByOrg(orgId) {
    const url = API_URLS.GET_RISKS_BY_ORG.replace('{orgId}', String(orgId))
    const response = await api.get(url)
    return response.data
}

export async function getAllRisks() {
    const response = await api.get(API_URLS.GET_ALL_RISKS)
    return response.data
}


