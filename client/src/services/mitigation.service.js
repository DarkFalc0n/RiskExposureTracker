import api from '@/config/axios'
import { API_URLS } from '@/constants/apiUrls'

export async function createMitigation(payload) {
    const response = await api.post(API_URLS.ADD_MITIGATION, payload)
    return response.data
}

export async function getMitigationsByRisk(riskId) {
    const url = API_URLS.GET_MITIGATIONS_BY_RISK.replace('{riskId}', String(riskId))
    const response = await api.get(url)
    return response.data
}

export async function getMitigationsByOrg(orgId) {
    const url = API_URLS.GET_MITIGATIONS_BY_ORG.replace('{orgId}', String(orgId))
    const response = await api.get(url)
    return response.data
}

export async function updateMitigation(id, payload) {
    const url = API_URLS.UPDATE_MITIGATION.replace('{id}', String(id))
    const response = await api.put(url, payload)
    return response.data
}


