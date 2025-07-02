// src/lib/types.ts

export interface Calories {
    sourceId: string;
    quantity: number;
    unit: string;
}

export interface Energy {
    sourceId: string;
    quantity: number;
    unit: string;
}

// Define the structure of a single food item
export interface FoodItem {
    calories?: Calories; // Optional, based on typical API responses
    constituents?: any[]; // You can define a more specific type if you need to use this data
    ediblePart?: { percent: number; sourceId: string };
    energy?: Energy;
    foodGroupId: string; // Use string as shown in snippet "12"
    foodId: string;       // Use string as shown in snippet "06.178"
    foodName: string;     // Corrected to camelCase
    langualCodes?: string[];
    latinName?: string;
    portions?: any[];
    searchKeywords?: string[];
    uri?: string;
    // Add any other properties you see in the console log
}

// Define the structure of the overall API response
export interface MatvaretabellenApiResponse {
    foods: FoodItem[]; // This is the array of food items
    locale: string;
}