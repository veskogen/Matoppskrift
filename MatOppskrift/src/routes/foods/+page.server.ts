import {error} from '@sveltejs/kit';
import type { MatvaretabellenApiResponse, FoodItem } from '$lib/types'; // Import both types

/** @type {import('./$types').PageServerLoad} */
export async function load({fetch}) {
    try {
        const response = await fetch('https://www.matvaretabellen.no/api/nb/foods.json');

        if (!response.ok) {
            throw error (response.status, `Failed to fetch foods: ${response.statusText}`);
        }

        // Parse the JSON and type it as MatvaretabellenApiResponse
        const apiResponse: MatvaretabellenApiResponse = await response.json(); 
        // The object returned by load needs to match the expected type in +page.svelte

        // Extract the 'foods' array from the apiResponse object
        const foods: FoodItem[] = apiResponse.foods;

        return {
            foods: foods // This 'foods' array will be passed to +page.svelte
       };
        
    }
    catch (err) {
        console.error('Error fetching foods:', err);
        throw error(500, 'Internal Server Error');
    }

}