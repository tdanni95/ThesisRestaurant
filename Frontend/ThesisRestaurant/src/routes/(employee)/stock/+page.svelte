<script lang="ts">
    import { page } from "$app/stores";
    import Input from "$lib/components/Input.svelte";
    import type {
        FoodSize,
        FoodType,
        Ingredient,
        IngredientType,
    } from "$lib/types/classData";
    import type { PageData } from "./$types";
    import FaSort from "svelte-icons/fa/FaSort.svelte";
    import SortTable from "$lib/components/SortTable.svelte";
    let isLoading = false;
    export let data: PageData;

    let ingredients: Array<Ingredient> = data.ingredients;
    let currentIngredient: Ingredient | undefined = undefined;

    let ingredientTypes: Array<IngredientType> = data.ingredientTypes;
    let foodTypes: Array<FoodType> = data.foodTypes;
    let foodSizes: Array<FoodSize> = data.foodSizes;
    console.log(foodSizes);

    const doFetch = (
        url: string,
        method: string,
        next: () => void,
        body?: {} | undefined
    ) => {};
</script>

<!-- ha lenne streamelt data -->
<!-- {#await data.streamed.ingredients}
    Loading....
{:then data}
    {#each data as ingredient}
        <p>{ingredient.name}</p>
    {/each}
{/await} -->
<!-- {#each ingredients as ingredient}
    <p>{ingredient.name}</p>
{/each} -->
<div class="min-w-screen flex items-center justify-center px-5 py-5">
    <div class="bg-gray-200 text-gray-500 rounded-3xl shadow-xl w-10/12">
        <div class="md:flex w-full">
            <div class="w-full md:w-6/12 py-10 px-5 md:px-10">
                <SortTable
                    columns={["name", "price", ["ingredientType", "name"]]}
                    ogArray={ingredients}
                />
            </div>
        </div>
    </div>
</div>

<div class="min-w-screen flex items-center justify-center px-5 py-5">
    <div class="bg-gray-200 text-gray-500 rounded-3xl shadow-xl w-10/12">
        <div class="md:flex w-full">
            <div class="w-full md:w-6/12 py-10 px-5 md:px-10">
                <SortTable
                    columns={["name", "minOption", "maxOption"]}
                    ogArray={ingredientTypes}
                />
            </div>
        </div>
    </div>
</div>

<div class="min-w-screen flex items-center justify-center px-5 py-5">
    <div class="bg-gray-200 text-gray-500 rounded-3xl shadow-xl w-10/12">
        <div class="md:flex w-full">
            <div class="w-full md:w-6/12 py-10 px-5 md:px-10">
                <SortTable columns={["name", "price"]} ogArray={foodTypes} />
            </div>
        </div>
    </div>
</div>

<div class="min-w-screen flex items-center justify-center px-5 py-5">
    <div class="bg-gray-200 text-gray-500 rounded-3xl shadow-xl w-10/12">
        <div class="md:flex w-full">
            <div class="w-full md:w-6/12 py-10 px-5 md:px-10">
                <SortTable
                    columns={["name", "multiplier", ["foodType", "name"]]}
                    ogArray={foodSizes}
                />
            </div>
        </div>
    </div>
</div>
