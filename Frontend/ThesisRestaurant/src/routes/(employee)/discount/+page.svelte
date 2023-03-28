<script lang="ts">
    import { page } from "$app/stores";
    import SortTable from "$lib/components/SortTable.svelte";
    import { ingredientFormatter } from "$lib/helpers/ingredientFormatter";
    import type {
        Food,
        FoodPicture,
        FoodPrice,
        FoodType,
        Ingredient,
    } from "$lib/types/classData";
    import type { FoodPriceErrors } from "$lib/types/errors";
    import type { SortTableFormatters } from "$lib/types/sortTableFormatters";
    import Input from "$lib/components/Input.svelte";
    import type { PageData } from "./$types";
    import Button from "$lib/components/Button.svelte";
    import toastStore from "$lib/stores/toastStore";
    import { invalidate } from "$app/navigation";
    import { toasts } from "$lib/stores";
    import Swal from "sweetalert2";

    let isLoading = false;
    export let data: PageData;

    $: foods = data.foods;

    let currentFood = {
        name: "",
        basePrice: 0,
        discountPrice: Number.MAX_SAFE_INTEGER,
        ingredients: [] as Array<Ingredient>,
        foodType: {} as FoodType,
        foodPictures: [] as Array<FoodPicture>,
        foodPrices: [] as Array<FoodPrice>,
    } as Food;

    let isCurrentFood = false;

    const foodIngredientsFormatter = (food: Food) => {
        return ingredientFormatter(food.ingredients);
    };

    const Formatter: SortTableFormatters<Food> = {
        name: "ingredients",
        callBack: foodIngredientsFormatter,
    };

    const priceFormatter = (food: Food) => {
        return Math.min(food.basePrice, food.discountPrice).toString();
    };

    const PriceFormatter: SortTableFormatters<Food> = {
        name: "price",
        callBack: priceFormatter,
    };

    const getCurrentFood = (id: number) => {
        const f = foods.find((food) => food.id === id);
        currentFood = JSON.parse(JSON.stringify(f));
        isCurrentFood = Boolean(f);
    };

    let currentFoodPrice: FoodPrice = {} as FoodPrice;
    let foodPriceErrors: FoodPriceErrors = {} as FoodPriceErrors;

    const saveFoodPrice = async () => {
        if (!currentFood.id) {
            toastStore.error("Select a food first!", 2000);
            return;
        }
        const request = {
            price: currentFoodPrice.price,
            from: currentFoodPrice.from,
            to: currentFoodPrice.to,
        };
        isLoading = true;
        const response = await fetch(`api/foodprice?id=${currentFood.id}`, {
            body: JSON.stringify(request),
            method: "POST",
        });
        const res = await response.json();
        console.log(res);

        if (res.title) {
            if (res.errors["Food.FoodPriceError"]) {
                toastStore.error(res.errors["Food.FoodPriceError"], 2000);
            } else {
                toastStore.error(res.title, 2000);
            }
            for (const error in res.errors) {
                foodPriceErrors[error] = res.errors[error];
            }
        } else {
            toastStore.success("Discount addedd successfully!", 2000);
        }
        isLoading = false;
        invalidateLoad();
        isCurrentFood = false;
    };

    const deleteDiscount = async (id: number) => {
        await Swal.fire({
            title: `Are you sure you want to delete the discount?`,
            icon: "question",
            showCancelButton: true,
            showLoaderOnConfirm: true,
            preConfirm: async () => {
                const resp = await fetch(`api/foodprice?id=${id}`, {
                    method: "DELETE",
                });
                const res = await resp.json();
                if (!res.title) {
                    toastStore.success("Deleted successfully", 2000);
                    isCurrentFood = false;
                    invalidateLoad();
                } else {
                    toastStore.error(res.title, 2000);
                }
            },
        });
    };

    const invalidateLoad = () => {
        invalidate("app:discount");
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
                    formatters={[Formatter, PriceFormatter]}
                    ogArray={foods}
                    editFunc={() => {}}
                    deleteFunc={() => {}}
                >
                    <td slot="actionButtons" class="text-center">
                        <!-- svelte-ignore a11y-click-events-have-key-events -->
                        <p
                            class="font-xl text-green-500 hover:text-green-700 transition-colors cursor-pointer"
                            on:click={(e) => {
                                const row = e.currentTarget.closest("tr");
                                if (!row || !row.dataset.id) return;
                                getCurrentFood(+row.dataset.id);
                            }}
                        >
                            Manage
                        </p>
                    </td>
                </SortTable>
            </div>
        </div>
    </div>
</div>
{#if isCurrentFood}
    <div class="min-w-screen flex items-center justify-center px-5 py-5">
        <div
            class="bg-gray-200 text-gray-500 rounded-3xl shadow-xl w-11/12 py-10"
        >
            <h1 class="font-bold text-3xl text-center text-gray-900">
                {currentFood.name} prices
            </h1>
            <div class="md:flex w-full">
                <div class="w-6/12 pb-10 px-5 md:px-10">
                    <SortTable
                        columns={["price", "from", "to"]}
                        ogArray={currentFood.foodPrices}
                        editFunc={() => {}}
                        deleteFunc={() => {}}
                    >
                        <td slot="actionButtons" class="text-center">
                            <!-- svelte-ignore a11y-click-events-have-key-events -->
                            <p
                                class="font-xl text-red-500 hover:text-red-700 transition-colors cursor-pointer"
                                on:click={async (e) => {
                                    const row = e.currentTarget.closest("tr");
                                    if (!row || !row.dataset.id) return;
                                    console.log(row.dataset.id);
                                    await deleteDiscount(+row.dataset.id);
                                }}
                            >
                                Delete
                            </p>
                        </td>
                    </SortTable>
                </div>
                <div class="w-6/12 pb-10 px-5 md:px-10">
                    <div class="flex -mx-3 mt-5 justify-center">
                        <h1 class="font-bold text-3xl text-gray-900">
                            Create discount
                        </h1>
                    </div>
                    <div class="flex -mx-3">
                        <Input
                            label="Price"
                            inputName="price"
                            bind:value={currentFoodPrice.price}
                            parentWidth="w-full"
                            error={foodPriceErrors.Price}
                            regex={/[^0-9.]/g}
                        />
                    </div>
                    <div class="flex -mx-3">
                        <Input
                            label="From"
                            inputName="from"
                            bind:value={currentFoodPrice.from}
                            parentWidth="w-full"
                            error={foodPriceErrors.From}
                            type="date"
                        />
                    </div>
                    <div class="flex -mx-3">
                        <Input
                            label="To"
                            inputName="to"
                            bind:value={currentFoodPrice.to}
                            parentWidth="w-full"
                            error={foodPriceErrors.To}
                            type="date"
                        />
                    </div>
                    <div class="flex mt-10">
                        <div class="w-full px-3 mb-5 max">
                            <Button
                                disabled={isLoading}
                                on:click={saveFoodPrice}>Save</Button
                            >
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
{/if}
