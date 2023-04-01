<script lang="ts">
    import { invalidate } from "$app/navigation";
    import CustomFoodForm from "$lib/components/CustomFoodForm.svelte";
    import CustomProductCard from "$lib/components/CustomProductCard.svelte";
    import SortTable from "$lib/components/SortTable.svelte";
    import { ingredientFormatter } from "$lib/helpers/ingredientFormatter";
    import toastStore from "$lib/stores/toastStore";
    import type {
        CustomFood,
        FoodType,
        Ingredient,
        IngredientType,
    } from "$lib/types/classData";
    import type { FoodErrors } from "$lib/types/errors";
    import type { SortTableFormatters } from "$lib/types/sortTableFormatters";
    import Button from "$lib/components/Button.svelte";
    import type { PageData } from "../$types";
    import cartStore from "$lib/stores/cartStore";

    export let data: PageData;

    let productDiv: HTMLDivElement;

    $: foods = data.customFoods as Array<CustomFood>;
    let isLoading = false;

    let currentFood = {
        name: "",
        price: 0,
        discountPrice: Number.MAX_SAFE_INTEGER,
        ingredients: [] as Array<Ingredient>,
        foodType: {} as FoodType,
    } as CustomFood;

    let isFoodEdit = false;

    $: ingredientTypes = data.ingredientTypes as Array<IngredientType>;
    $: ingredients = data.ingredients as Array<Ingredient>;
    $: foodTypes = data.foodTypes as Array<FoodType>;

    const foodIngredientsFormatter = (food: CustomFood) => {
        return ingredientFormatter(food.ingredients);
    };

    const invalidateLoad = () => {
        invalidate("app:customFood");
    };

    const Formatter: SortTableFormatters<CustomFood> = {
        name: "ingredients",
        callBack: foodIngredientsFormatter,
    };

    const createCustomFood = async () => {
        // string Name, double BasePrice, int FoodTypeId, List<int> IngredientIds
        const json = {
            name: currentFood.name,
            foodTypeId: currentFood.foodType.id,
            IngredientIds: currentFood.ingredients.map((i) => i.id),
        };
        const response = await fetch(`api/customfood`, {
            body: JSON.stringify(json),
            method: "POST",
        });
        const res = await response.json();

        if (res.errors) {
            handleFoodErrors(res);
        } else {
            toastStore.success("Custom food created successfully", 2000);
            resetCurrentFood();
            invalidateLoad();
        }
    };
    let errors: FoodErrors = {} as FoodErrors;
    $: countErrors = [] as Array<string>;

    const handleFoodErrors = (res: any) => {
        toastStore.error(res.title, 2000);
        for (const err in res.errors) {
            if (err.includes("CountError")) {
                countErrors.push(res.errors[err]);
            }
            errors[err] = res.errors[err];
        }
    };

    const resetCurrentFood = () => {
        currentFood = {
            name: "",
            price: 0,
            discountPrice: Number.MAX_SAFE_INTEGER,
            ingredients: [] as Array<Ingredient>,
            foodType: {} as FoodType,
        } as CustomFood;
    };
</script>

<div class="min-w-screen flex items-center justify-center px-5 py-5">
    <div class="bg-gray-200 text-gray-500 rounded-3xl shadow-xl w-11/12 py-10">
        <h1 class="font-bold text-3xl text-center text-gray-900">Foods</h1>
        <div class="md:flex w-full">
            <div class="w-full pb-10 px-5 md:px-10">
                <SortTable
                    columns={[
                        "name",
                        "price",
                        ["foodType", "name"],
                        "ingredients",
                    ]}
                    formatters={[Formatter]}
                    ogArray={foods}
                >
                    <th
                        slot="actionButtons"
                        scope="row"
                        class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap"
                    >
                        <!-- svelte-ignore a11y-click-events-have-key-events -->
                        <p
                            on:click={(e) => {
                                const tr = e.currentTarget.closest('tr')
                                if(!tr) return
                                const id = tr.dataset.id
                                if(!id) return
                                try {
                                    cartStore.addCustomitem(+id);
                                    toastStore.success("Added to cart successfully!", 2000)
                                } catch (error) {
                                    toastStore.error("Unexpected error!", 2000)
                                }
                            }}
                            class="text-green-500 cursor-pointer hover:text-green-700"
                        >
                            Add to cart
                        </p>
                    </th>
                </SortTable>
            </div>
        </div>
    </div>
</div>
<div class="min-w-screen flex items-center justify-center px-5 py-5">
    <div class="bg-gray-200 text-gray-500 rounded-3xl shadow-xl w-11/12 py-10">
        <h1 class="font-bold text-3xl text-center text-gray-900">Foods</h1>
        {#if countErrors.length}
            <h3 class="font-bold text-2xl text-center text-red-500">
                Ingredient errors:
            </h3>
            {#each countErrors as error}
                <p class="font-bold text-xl text-center text-red-500">
                    {error}
                </p>
            {/each}
        {/if}
        <div class="md:flex w-full mt-5" bind:this={productDiv}>
            <div class="w-full md:w-6/12 pb-10 px-5 md:px-10">
                <div class="flex mt-5 mb-5">
                    <h1 class="font-bold text-3xl text-gray-900">
                        Food preview
                    </h1>
                </div>
                <CustomProductCard
                    bind:food={currentFood}
                    ingredient={ingredientFormatter(currentFood?.ingredients)}
                />
            </div>
            <div class="w-full md:w-6/12 pb-10 px-5 md:px-10">
                <div class="flex mt-5 mb-5">
                    <h1 class="font-bold text-3xl text-gray-900">Food data</h1>
                </div>
                <CustomFoodForm
                    {errors}
                    bind:currentFood
                    {ingredients}
                    {foodTypes}
                    {ingredientTypes}
                >
                    <div class="mt-5 w-full" slot="buttons">
                        <Button disabled={isLoading} on:click={createCustomFood}
                            >Save</Button
                        >
                    </div>
                </CustomFoodForm>
            </div>
        </div>
    </div>
</div>
