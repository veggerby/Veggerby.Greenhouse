import { handleJsonResponse, handleError } from "./apiUtils";

const baseUrl = "api/measurements?";

export async function get(token, sensors, property) {
    try {
        if (!(sensors && property)) {
            return;
        }

        var params = sensors.map(s => { return { k: 's', v: s.key }});
        params.push({ k: 'p', v: property.id });
        params.push({ k: 'h', v: 2 * 24 });

        var query = params
            .map(p => encodeURIComponent(p.k) + '=' + encodeURIComponent(p.v))
            .join('&');

        const response = await fetch(baseUrl + query, {
            headers: { Accept: "application/json", Authorization: `Bearer ${token}` },
        });
        return handleJsonResponse(response);
    } catch (error) {
        handleError(error);
    }
}