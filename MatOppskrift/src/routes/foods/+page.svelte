<script lang="ts">
 // Import PageData from $types, which SvelteKit automatically generates
    // based on the return type of your +page.server.js load function.
    import type { PageData } from './$types'; 
    import type { FoodItem } from '$lib/types'; // Still good to import for local type safety if needed


    //access the data returned from +page.server.js
// SvelteKit automatically infers the type of 'data' from the load function
    // as 'PageData'. You can explicitly type it for clarity and safety.
    export let data: PageData; 
    
    // Now, TypeScript knows that data.foods is an array of FoodItem
    const foods: FoodItem[] = data.foods;
    //foods itself is an array [], and inside that array are many {} (object) entries.
    console.log(foods); // Logs only the first element in the array
    console.log('Number of food items:', foods.length); 
 </script>

 
<h1>Matvaretabellen Foods</h1>


{#if foods.length === 0}
    <p>No food data available or the array is empty.</p>
{:else}
    <ul>
        {#each foods as food (food.foodId)} 
            <li>
                {food.foodName}
                {#if food.calories}
                    - {food.calories.quantity} {food.calories.unit}
                {/if}
            </li>
        {/each}
    </ul>
{/if}



<style>
    h1 {
        color: #333;
        font-size: 2em;
        margin-bottom: 1em;
    }
    ul {
        list-style-type: none;
        padding: 0;
    }
    li {
        background-color: #f9f9f9;
        margin: 0.5em 0;
        padding: 0.5em;
        border-radius: 4px;
    }
</style>
<!-- This Svelte component displays a list of foods fetched from the server.
It checks if the foods array is not empty and then iterates over each food item to display
its name. If no foods are found, it displays a message indicating that no food items were found.
The foods are expected to have a FoodCode and FoodName property, which are used for
displaying the food name and for the key in the each block to ensure efficient rendering. -->
