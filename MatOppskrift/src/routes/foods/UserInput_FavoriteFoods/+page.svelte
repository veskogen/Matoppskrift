<script lang="ts">
//User selected favorite foods table

    //Data structure e.g., FoodItem types
   import type { PageData } from './$types'; 
   import type { FoodItem } from '$lib/types';
   import { favoriteFoods } from '$lib/stores/UserInput_FavoriteFoods';

    let rows: { name: string, age: number }[] = [];

</script>




<h1>Favoritter</h1>


{#if $favoriteFoods.length === 0}
    <p>Ingen favoritter å vise enda.</p>
{:else}
    <table>
        <thead>
            <tr>
                <th>Navn</th>
                <th>Kalorier</th>
            </tr>
        </thead>
        <tbody>
            {#each $favoriteFoods as food (food.foodId)}
                <tr>
                    <td>{food.foodName}</td>
                    <td>{food.calories ? food.calories.quantity : '-'}</td>
                </tr>
            {/each}
        </tbody>
    </table>
{/if}

<style>
  table {
    border-collapse: collapse;
    width: 100%;
    border: 1px solid #ddd;
  }
  th, td {
    border: 1px solid #ddd;
    padding: 8px;
    text-align: left;
  }
  th {
    background-color: #f2f2f2;
  }
</style>