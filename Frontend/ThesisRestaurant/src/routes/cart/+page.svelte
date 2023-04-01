<script lang="ts">
    import cartStore from "$lib/stores/cartStore";
    import { flip } from "svelte/animate";
    import { fade } from "svelte/transition";
    import { page } from "$app/stores";
    import type { UserData } from "$lib/types/userData";
    import { onMount } from "svelte";
    import type {
        CartCustomFood,
        CartFood,
        CartIngredient,
        CartResponse,
    } from "$lib/types/classData";
    import SortTable from "$lib/components/SortTable.svelte";
    import type { SortTableFormatters } from "$lib/types/sortTableFormatters";
    import type { CartItem } from "$lib/types/cart";
    import Button from "$lib/components/Button.svelte";
    import toastStore from "$lib/stores/toastStore";
    import { redirect } from "@sveltejs/kit";

    let userData = $page.data.userData as UserData;

    let selectedAddressId = userData.addresses[0].id;

    let foods: CartResponse | undefined = undefined;

    let totalPrice = 0;
    let isLoading = false;

    onMount(async () => {
        await fetchCart();
    });

    const fetchCart = async () => {
        const body = {
            customFoods: $cartStore.customFoods.map((cf) => cf.id),
            foods: $cartStore.foods,
        };
        const response = await fetch("api/cart", {
            body: JSON.stringify(body),
            method: "POST",
        }).then(async (res) => await res.json());
        foods = response.cart as CartResponse;

        for (const food of $cartStore.foods) {
            let myFood: CartFood | undefined = foodMatcher(food);
            if (myFood) {
                myFood.guid = food.guid;
            }
        }

        for (const food of $cartStore.customFoods) {
            let myFood: CartCustomFood | undefined = foods.customFoods.find(
                (f) => f.id == food.id
            );
            if (myFood) {
                myFood.guid = food.guid;
            }
        }

        totalPrice = 0;
        totalPrice += foods.foods.reduce(
            (carry, current) => carry + current.price,
            0
        );
        totalPrice += foods.customFoods.reduce(
            (carry, current) => carry + current.price,
            0
        );
    };

    const deleteFoodFromCart = async (guid: number) => {
        if (!foods) return;
        let myFood = foods.foods.find((f) => f.guid === guid);
        if (!myFood) return;
        // foods = undefined;
        cartStore.deleteItem(guid);
        await fetchCart();
    };

    const deleteCustomFoodFromCart = async (guid: number) => {
        if (!foods) return;
        let myFood = foods.customFoods.find((f) => f.guid === guid);
        console.log(myFood);

        if (!myFood) return;
        // foods = undefined;
        cartStore.deleteItem(guid, true);
        await fetchCart();
    };

    const foodMatcher = (cartItem: CartItem) => {
        if (!foods) return;
        if (cartItem.additionalIngredients.length === 0) {
            let myFood = foods.foods.find(
                (f) =>
                    f.id == cartItem.id &&
                    f.foodSizeId == cartItem.foodSizeId &&
                    f.additionalIngredients.length === 0
            );
            return myFood;
        } else {
            let myFood = foods.foods.find(
                (f) =>
                    f.id === cartItem.id &&
                    f.foodSizeId == cartItem.foodSizeId &&
                    arrayOfIdsEqual(
                        cartItem.additionalIngredients,
                        f.additionalIngredients.map((ai) => ai.id)
                    )
            );

            return myFood;
        }
    };

    const arrayOfIdsEqual = (a: Array<number>, b: Array<number>) => {
        for (let i = 0; i < a.length; i++) {
            if (!b.find((id) => id === a[i])) return false;
        }

        return true;
    };

    const foodIngredientsFormatter = (food: CartFood) => {
        return food.ingredients.map((i) => i.name).join(", ");
    };

    const IngredientFormatter: SortTableFormatters<CartFood> = {
        name: "ingredients",
        callBack: foodIngredientsFormatter,
    };

    const additionalIngredientFormatter = (food: CartFood) => {
        return food.additionalIngredients
            .map(
                (i) => `${i.name} - ${i.price * food.foodSizeMultiplier}Ft<br>`
            )
            .join(", ");
    };

    const AdditionalIngredientFormatter: SortTableFormatters<CartFood> = {
        name: "additionalIngredients",
        callBack: additionalIngredientFormatter,
    };

    const priceFormatter = (food: CartFood | CartCustomFood) => {
        return `${food.price} Ft`;
    };

    const PriceFormatter: SortTableFormatters<CartFood | CartCustomFood> = {
        name: "price",
        callBack: priceFormatter,
    };

    const placeOrder = async () => {
        isLoading = true;
        const body = {
            addressId: selectedAddressId,
            customFoods: $cartStore.customFoods.map((cf) => cf.id),
            foods: $cartStore.foods,
        };
        const response = await fetch("api/order", {
            body: JSON.stringify(body),
            method: "POST",
        }).then(async (res) => await res.json());

        if (response.msg) {
            //TODO clear cart
            toastStore.success("Items ordered successfully", 2000);
            cartStore.clear();
            foods!.customFoods = [] as Array<CartCustomFood>;
            foods!.foods = [] as Array<CartFood>;
        } else {
            toastStore.error(response.title);
        }
        console.log(response);

        isLoading = false;
    };
</script>

<br />

{#if foods}
    <div class="min-w-screen flex items-center justify-center px-5 py-5">
        <div
            class="bg-gray-200 text-gray-500 rounded-3xl shadow-xl w-11/12 py-10"
        >
            <h1 class="font-bold text-3xl text-center text-gray-900">
                Foods in cart 🍕
            </h1>
            <div class="md:flex w-full">
                <div class="w-full pb-10 px-5 md:px-10">
                    <SortTable
                        columns={[
                            "name",
                            "foodType",
                            "foodSize",
                            "quantity",
                            "ingredients",
                            "additionalIngredients",
                            "price",
                        ]}
                        idKey={"guid"}
                        formatters={[
                            IngredientFormatter,
                            AdditionalIngredientFormatter,
                            PriceFormatter,
                        ]}
                        ogArray={foods.foods}
                        deleteFunc={() => {}}
                        editFunc={() => {}}
                    >
                        <td slot="actionButtons" class="text-center">
                            <!-- svelte-ignore a11y-click-events-have-key-events -->
                            <p
                                class="text-red-500 hover:text-red-700 transition-colors cursor-pointer"
                                on:click={(e) => {
                                    const tr = e.currentTarget.closest("tr");
                                    if (!tr) return;
                                    const id = tr.dataset.id;
                                    if (!id) return;
                                    deleteFoodFromCart(+id);
                                }}
                            >
                                Delete
                            </p>
                        </td>
                    </SortTable>
                </div>
            </div>
        </div>
    </div>
    <div class="min-w-screen flex items-center justify-center px-5 py-5">
        <div
            class="bg-gray-200 text-gray-500 rounded-3xl shadow-xl w-11/12 py-10"
        >
            <h1 class="font-bold text-3xl text-center text-gray-900">
                Custom Foods in cart 🍕
            </h1>
            <div class="md:flex w-full">
                <div class="w-full pb-10 px-5 md:px-10">
                    <SortTable
                        columns={[
                            "name",
                            "foodType",
                            "quantity",
                            "ingredients",
                            "price",
                        ]}
                        idKey={"guid"}
                        formatters={[IngredientFormatter, PriceFormatter]}
                        ogArray={foods.customFoods}
                        deleteFunc={() => {}}
                        editFunc={() => {}}
                    >
                        <td slot="actionButtons" class="text-center">
                            <!-- svelte-ignore a11y-click-events-have-key-events -->
                            <p
                                class="text-red-500 hover:text-red-700 transition-colors cursor-pointer"
                                on:click={(e) => {
                                    const tr = e.currentTarget.closest("tr");
                                    if (!tr) return;
                                    const id = tr.dataset.id;
                                    if (!id) return;
                                    deleteCustomFoodFromCart(+id);
                                }}
                            >
                                Delete
                            </p>
                        </td>
                    </SortTable>
                </div>
            </div>
        </div>
    </div>
{:else}
    <div class="flex justify-center w-full text-center">
        <h1 class="text-3xl">Loading...</h1>
    </div>
{/if}

<div class="min-w-screen flex items-center justify-center px-5 py-5">
    <div class="bg-gray-200 text-gray-500 rounded-3xl shadow-xl w-11/12 py-10">
        <h1 class="font-bold text-3xl text-center text-gray-900">
            Select address then place order
        </h1>

        <div class="flex justify-center w-8/12 mx-auto">
            {#each userData.addresses as address (address.id)}
                <!-- svelte-ignore a11y-click-events-have-key-events -->
                <div
                    class="addressCard"
                    on:click={() => (selectedAddressId = address.id)}
                    class:selected={address.id === selectedAddressId}
                >
                    {address.zipCode}, {address.city}
                    {address.street}
                    {address.houseNumber}
                </div>
            {/each}
        </div>
        <div class="flex justify-center w-8/12 mx-auto mt-6">
            <Button
                on:click={placeOrder}
                disabled={totalPrice == 0 || isLoading}
                >Place order - {totalPrice} Ft</Button
            >
        </div>
    </div>
</div>

<style lang="scss">
    .addressCard {
        display: grid;
        border: 1px solid black;
        border-radius: 10px;
        margin: 10px;
        padding: 20px;
        transition: background-color 0.3s ease-in-out;
        cursor: pointer;
        &.selected {
            @apply bg-red-500;
            color: #fff;
            // background-color: greenyellow;
        }

        &:hover:not(.selected) {
            @apply bg-red-400;
            color: #fff;
            // background-color: lightblue;
        }
    }
</style>
