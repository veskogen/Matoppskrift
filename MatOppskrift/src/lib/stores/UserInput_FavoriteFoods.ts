import { writable } from 'svelte/store';
import type { FoodItem } from '../types'; // Adjust the path as necessary


export const favoriteFoods = writable<FoodItem[]>([]);