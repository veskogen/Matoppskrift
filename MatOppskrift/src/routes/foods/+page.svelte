<!-- This Svelte component displays a list of foods fetched from the server.
It checks if the foods array is not empty and then iterates over each food item to display
its name. If no foods are found, it displays a message indicating that no food items were found.
The foods are expected to have a FoodCode and FoodName property, which are used for
displaying the food name and for the key in the each block to ensure efficient rendering. -->

<script lang="ts">

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

let search = '';
$: filteredFoods = foods.filter(food=>food.foodName.toLowerCase().includes(search.toLowerCase()));

let sortBy: 'name' | 'calories' = 'name';
let sortAsc = true;
$: sortedFoods = [...filteredFoods].sort((a,b) => {
    let result = 0;
    if (sortBy === 'name') {
        result = a.foodName.localeCompare(b.foodName);
    }
    else if (sortBy === 'calories') {
        //Handle missing calories
        const aCalories = a.calories ? a.calories.quantity : 0;
        const bCalories = b.calories ? b.calories.quantity : 0;
        result = aCalories - bCalories;
    }
    return sortAsc ? result : -result; // Default case (should not reach here)
});

    function handleSort(column: 'name' | 'calories') {
    if (sortBy === column) {
        sortAsc = !sortAsc; // Toggle sort direction
    } else {
        sortBy = column;
        sortAsc = true; // Default to ascending when changing column
    }
}
 </script>


 
<h1>Matvaretabellen Foods</h1>

<div class="info">
    <p>Viser {sortedFoods.length} matvarer</p>
</div>

<div class=table-controls>
    <input 
        type="text"
        placeholder="Søk etter matvare"
        bind:value={search}
        style="margin-bottom: 1em; padding: 0.5em; width: 10%; max-width: 400px;"
    />
</div>

{#if sortedFoods.length === 0}
    <p>No food data available or the array is empty.</p>
{:else}
    <table>
        <thead>
            <tr>
                <th on:click={() => handleSort('name')} style="cursor:pointer">
                    Navn {sortBy === 'name' ? (sortAsc ? '▲' : '▼') : ''}
                </th>
                <th on:click={() => handleSort('calories')} style="cursor:pointer">
                    Kalorier {sortBy === 'calories' ? (sortAsc ? '▲' : '▼') : ''}
                </th>
            </tr>
        </thead>
        <tbody>
            {#each sortedFoods as food (food.foodId)}
                <tr>
                    <td>{food.foodName}</td>
                    <td>{food.calories ? food.calories.quantity : '-'}</td>
                </tr>
            {/each}
        </tbody>
    </table>
{/if}



<style>
    h1 {
        color: #333;
        font-size: 2em;
        margin-bottom: 1em;
    }
    .table-controls {
        margin-bottom: 1em;
        display: flex;
        align-items: center;
    }
    table {
        width: 100%;
        border-collapse: collapse;
        margin-top: 1em;
    }
    th, td {
        border: 1px solid #ddd;
        padding: 0.75em;
        text-align: left;
    }
    th {
        background-color: #f2f2f2;
    }
    tr:nth-child(even) {
        background-color: #fafafa;
    }
</style>
