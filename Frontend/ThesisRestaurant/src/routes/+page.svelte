<script lang="ts">
    import { page } from "$app/stores";
    import Button from "$lib/components/Button.svelte";
    import Modal from "$lib/components/Modal.svelte";
    import ProductCard from "$lib/components/ProductCard.svelte";
    import IngredientForm from "$lib/components/stockForms/IngredientForm.svelte";
    import { ingredientFormatter } from "$lib/helpers/ingredientFormatter";
    import type { CartItem } from "$lib/types/cart";
    import type { Food, FoodSize, Ingredient } from "$lib/types/classData";
    import type { IngredientCheckboxes } from "$lib/types/ingredientCheckboxes";
    import { fade, fly, slide } from "svelte/transition";
    import type { PageData } from "./$types";

    export let data: PageData;

    let currentFoodType = data.foodTypes[0];
    let foods = data.foods;

    $: displayFoods = foods.filter(
        (food) => food.foodType.id === currentFoodType.id
    );

    $: foodSizes = data.foodSizes.filter(
        (fs) => fs.foodType.id === currentFoodType.id
    );

    let modalVisible = false;
    let foodToAdd: Food | undefined = undefined;
    let foodSizeToAdd: FoodSize | undefined = undefined;
    let selectedIngredients: Array<Ingredient> = [];
    let additionalPrice = 0;
    let currentFoodPrice = 0;

    const toCartClick = (foodId: number, foodSizeId: number) => {
        foodToAdd = foods.find((f) => f.id === foodId);
        foodSizeToAdd = data.foodSizes.find((fs) => fs.id === foodSizeId);
        if (!foodToAdd || !foodSizeToAdd) return;
        currentFoodPrice =
            foodToAdd.basePrice < foodToAdd.discountPrice
                ? foodToAdd.basePrice
                : foodToAdd.discountPrice;
        currentFoodPrice *= foodSizeToAdd.multiplier;
        resetDefaults();
        buildCheckBoxes();
    };

    const resetDefaults = () => {
        selectedIngredients = [];
        additionalPrice = 0;
        modalVisible = true;
    };

    const handleIngredientChange = (id: number, checked: boolean) => {
        const ingredient = data.ingredients.find((i) => i.id === id);
        if (!ingredient) return;
        if (checked) {
            selectedIngredients = [...selectedIngredients, ingredient];
            additionalPrice += ingredient.price;
        } else {
            selectedIngredients = selectedIngredients.filter(
                (i) => i.id !== id
            );
            additionalPrice -= ingredient.price;
        }
    };

    let checkBoxes: Array<IngredientCheckboxes> = [];
    const buildCheckBoxes = () => {
        if (!foodToAdd) return;
        checkBoxes = [];
        for (const type of data.ingredientTypes) {
            let isSaved = checkBoxes.find((c) => c.name === type.name);
            let filtered = data.ingredients.filter(
                (ingredient) =>
                    ingredient.ingredientType.id === type.id &&
                    !foodToAdd!.ingredients.find((i) => i.id === ingredient.id)
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
    };

    const addCurrentFoodToCart = () => {
        let cartItem: CartItem = {
            id: foodToAdd!.id,
            foodSizeId: foodSizeToAdd!.id,
            additionalIngredients: selectedIngredients.map((i) => i.id),
        };

        console.log(cartItem);
        //TODO push item to cart
    };
</script>

{#if modalVisible}
    <Modal bind:visible={modalVisible} close={() => (modalVisible = false)}>
        <div slot="header">
            <h2 class="text-gray-900 font-bold text-xl mb-4">
                {foodSizeToAdd?.name}: {foodToAdd?.name}
            </h2>
            <h3 class="text-red-500 font-bold text-xl">
                {currentFoodPrice} + {additionalPrice} = {currentFoodPrice +
                    additionalPrice} Ft
            </h3>
        </div>
        <div slot="body" class=" max-h-96 overflow-auto p-2">
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
                                    (ing) => ing.id == ingredient.id
                                ) !== undefined}
                                class="w-5 h-5 text-blue-600 bg-gray-100 border-gray-300 rounded focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600"
                            />
                            <label
                                for="check-{ingredient.id}"
                                class="ml-2 text-sm font-medium text-gray-900 dark:text-gray-300"
                                >{ingredient.name} - {ingredient.price} Ft</label
                            >
                        </div>
                    {/each}
                </div>
            {/each}
        </div>
        <div slot="footer" class="flex mt-3">
            <Button
                btnClass="btn-primary"
                on:click={() => {
                    modalVisible = false;
                }}>Close</Button
            >
            <Button btnClass="btn-success" on:click={addCurrentFoodToCart}
                >Add to cart</Button
            >
        </div>
    </Modal>
{/if}


<div class="foodTypeSelector mt-10 w-8/12 mx-auto">
    {#each data.foodTypes as foodType}
        <!-- svelte-ignore a11y-click-events-have-key-events -->
        <div
            on:click={() => {
                currentFoodType = foodType;
            }}
            class:selected={currentFoodType.id == foodType.id}
        >
            {foodType.name}
        </div>
    {/each}
</div>
{#if displayFoods}
    <div class="productGrid w-8/12 mx-auto mt-10">
        {#each displayFoods as food (food.id)}
            <div class="w-full" in:fly={{duration: 2000}}>
                <ProductCard
                    {toCartClick}
                    {food}
                    availableFoodSizes={foodSizes}
                    ingredient={ingredientFormatter(food.ingredients)}
                />
            </div>
        {/each}
    </div>
{/if}

<style lang="scss">
    .productGrid {
        display: grid;
        grid-template-columns: repeat(3, 1fr);
        gap: 15px;
        @media screen and (max-width: 1500px) {
            grid-template-columns: repeat(2, 1fr);
        }
        @media screen and (max-width: 1000px) {
            grid-template-columns: repeat(1, 1fr);
        }
    }

    .checkGrid {
        display: grid;
        grid-template-columns: repeat(3, 1fr);
        gap: 5px;
    }

    .foodTypeSelector {
        display: grid;
        grid-template-columns: repeat(5, 1fr);
        justify-content: center;
        align-items: center;

        .selected {
            color: #000;
            opacity: 1;
            &::before {
                visibility: visible;
                transform: scaleX(1);
                background-color: #000;
            }
        }

        div {
            display: inline-block;
            width: 100%;
            position: relative;
            text-decoration: none;
            text-align: center;
            cursor: pointer;
            opacity: 0.6;
            transition: all 0.3s ease-in-out 0s;

            &:hover {
                color: #000;
                opacity: 1;

                &::before {
                    visibility: visible;
                    transform: scaleX(1);
                }
            }

            &::before {
                content: "";
                position: absolute;
                display: inline-block;
                width: 100%;
                height: 2px;
                bottom: -6px;
                left: 0;
                background-color: darkgrey;
                visibility: hidden;
                transform: scaleX(0);
                transition: all 0.3s ease-in-out 0s;
            }
        }
    }
</style>
