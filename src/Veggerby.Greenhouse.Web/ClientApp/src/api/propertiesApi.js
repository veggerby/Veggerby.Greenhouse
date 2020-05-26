import { handleJsonResponse, handleError } from "./apiUtils";

const baseUrl = "api/properties";

export async function get(token) {
    try {
        const response = await fetch(baseUrl, {
            headers: { Accept: "application/json", Authorization: `Bearer ${token}` },
        });
        return handleJsonResponse(response);
    } catch (error) {
        handleError(error);
    }
}