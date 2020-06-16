export async function handleJsonResponse(response) {
    if (response.ok) {
        if (response.status === 204) {
            return null;
        }

        return response.json();
    } else {
        if (response.status === 401) {
            console.log('Unauthorized');
            return null;
        }

        throw new Error(`Error calling api - ${response.statusText}`);
    }
}

export async function handleEmptyResponse(response) {
    if (response.ok) {
        return true;
    } else {
        throw new Error(`Error calling api - ${response.statusText}`);
    }
}

// In a real app, would likely call an error logging service.
export function handleError(error) {
    // eslint-disable-next-line no-console
    console.error("API call failed. " + error);
    throw error;
}