<script lang="ts">
    import type {
        Food,
        FoodType,
        Ingredient,
        IngredientType,
    } from "$lib/types/classData";
    import Input from "$lib/components/Input.svelte";
    import Select from "$lib/components/Select.svelte";
    import type { IngredientCheckboxes } from "$lib/types/ingredientCheckboxes";
    import { fade } from "svelte/transition";
    import type { FoodErrors } from "$lib/types/errors";
    import Button from "./Button.svelte";

    export let currentFood: Food;
    export let ingredients: Array<Ingredient>;
    export let ingredientTypes: Array<IngredientType>;
    export let foodTypes: Array<FoodType>;
    export let errors: FoodErrors;

    $: selectedIngredients = currentFood.ingredients.map(
        (ingredient) => ingredient.id
    );

    let ingredientsHidden = false;

    const handleIngredientChange = (id: number, checked: boolean) => {
        if (checked) {
            const ingredient = ingredients.find((i) => i.id === id);
            if (!ingredient) return;
            currentFood.ingredients = [...currentFood.ingredients, ingredient];
        } else {
            currentFood.ingredients = currentFood.ingredients.filter(
                (i) => i.id !== id
            );
        }
    };

    const handleSelection = (id: number) => {
        const myType = foodTypes.filter((ft) => ft.id == id);
        if (myType && myType[0]) {
            currentFood.foodType = myType[0];
        } else {
            currentFood.foodType = {} as FoodType;
        }
    };

    let checkBoxes: Array<IngredientCheckboxes> = [];
    for (const type of ingredientTypes) {
        let isSaved = checkBoxes.find((c) => c.name === type.name);
        let filtered = ingredients.filter(
            (ingredient) => ingredient.ingredientType.id === type.id
        );
        if (!isSaved) {
            checkBoxes.push({
                name: `${type.name} - maximum: ${type.maxOption}`,
                ingredients: filtered,
            });
        } else {
            isSaved.ingredients = filtered;
        }
    }
</script>

<div class="flex -mx-3">
    <Input
        bind:value={currentFood.name}
        inputName="name"
        label="Name"
        parentWidth="w-full"
        required={true}
        error={errors.Name}
    />
</div>
<div class="flex -mx-3">
    <Input
        bind:value={currentFood.basePrice}
        inputName="basePrice"
        label="Base price"
        parentWidth="w-full"
        required={true}
        regex={/[^0-9.]/g}
        error={errors.BasePrice}
    />
</div>
<div class="flex -mx-3">
    <Select
        inputName={"foodType"}
        label={"Food type"}
        bind:data={foodTypes}
        value={currentFood.foodType.id}
        parentWidth={"w-full"}
        on:selectionchange={(e) => {
            handleSelection(+e.detail.id);
        }}
        error={errors.FoodTypeId}
    />
</div>
<div class="flex w-full -ml-6 mr-2">
    <slot name="buttons" />
</div>
{#if errors.IngredientIds}
    <h1 class="font-2xl font-red-500">{errors.IngredientIds}</h1>
{/if}
<div class="border-gray-50 rounded-lg bg-gray-100 p-6 mt-5 -mx-3 w-full">
    <div class="flex w-full -ml-6 mr-2 mt-5 justify-between px-6">
        <div>
            <p class="font-bold text-2xl">Ingredients:</p>
        </div>
        <div>
            <Button
                on:click={() => {
                    ingredientsHidden = !ingredientsHidden;
                }}>{ingredientsHidden ? "Show" : "Hide"} ingredients</Button
            >
        </div>
    </div>
    {#if !ingredientsHidden}
        <div transition:fade>
            {#each checkBoxes as checkbox}
                <p class="font-bold text-xl">{checkbox.name}:</p>
                <div class="checkGrid">
                    {#each checkbox.ingredients as ingredient (ingredient.id)}
                        <div class="flex items-center mr-4">
                            <input
                                id="check-{ingredient.id}"
                                type="checkbox"
                                value={ingredient.id}
                                on:change={(e) => {
                                    handleIngredientChange(
                                        +e.currentTarget.value,
                                        e.currentTarget.checked
                                    );
                                }}
                                checked={selectedIngredients.find(
                                    (ing) => ing == ingredient.id
                                ) !== undefined}
                                class="w-5 h-5 text-blue-600 bg-gray-100 border-gray-300 rounded focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600"
                            />
                            <label
                                for="check-{ingredient.id}"
                                class="ml-2 text-sm font-medium text-gray-900 dark:text-gray-300"
                                >{ingredient.name}</label
                            >
                        </div>
                    {/each}
                </div>
            {/each}
        </div>
    {/if}
</div>

<style lang="scss">
    .checkGrid {
        display: grid;
        grid-template-columns: repeat(2, 1fr);
        gap: 5px;
    }
</style>
