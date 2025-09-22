export const API_URLS = {
    LOGIN: `/auth/login`,
    REGISTER: `/auth/register`,
    ME: `/auth/me`,
    ADD_RISK: `/risks`,
    UPDATE_RISK: `/risks/{id}`,
    ADD_MITIGATION: `/mitigations`,
    UPDATE_MITIGATION: `/mitigations/{id}`,
    GET_ALL_MITIGATIONS: `/mitigations`,
    GET_MITIGATIONS_BY_RISK: `/mitigations/{riskId}`,
    GET_ALL_ORGANIZATIONS: `/orgs`,
    GET_ALL_RISKS: `/risks`,
    GET_RISKS_BY_ORG: `/risks/{orgId}`,
};