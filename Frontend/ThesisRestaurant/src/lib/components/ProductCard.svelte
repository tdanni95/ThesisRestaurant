<script lang="ts">
    import type { Food, FoodSize } from "$lib/types/classData";
    import { Card } from "flowbite-svelte";
    import { onDestroy, onMount } from "svelte";
    import { slide } from "svelte/transition";
    import Button from "./Button.svelte";

    export let food: Food;

    export let availableFoodSizes: Array<FoodSize> = [];

    $: name = food.name;
    export let ingredient: string;

    export let toCartClick: (foodId: number, foodTypeId: number) => void = () => {}

    $: foodType = food.foodType.name;
    $: isDiscounted = food.discountPrice < food.basePrice;

    //make current image lenth reactive -> food changes, currentImage also changes
    $: idx = food.foodPictures.length - food.foodPictures.length;
    $: currentImage = food.foodPictures[idx].id;

    let to: NodeJS.Timer;

    onMount(() => {
        to = setInterval(() => {
            if (idx == food.foodPictures.length - 1) idx = 0;
            else idx++;

            currentImage = food.foodPictures[idx].id;
        }, 3000);
    });

    onDestroy(() => {
        clearInterval(to);
    });
</script>

<Card class="w-full" padding="none" size="md">
    <!-- <img class="p-8 rounded-t-lg" {src} alt={name} /> -->
    <div class="text-center w-full">
        <h1 class="font-bold text-2xl font-xl">
            {foodType || "Choose a foodtype"}
        </h1>
    </div>
    <hr class="my-1" />
    {#if food.foodPictures && food.foodPictures.length}
        {#each food.foodPictures as picture (picture.id)}
            <div class="w-full flex justify-center items-center">
                {#if currentImage === picture.id}
                    <div
                        transition:slide|local
                        class="overflow-hidden max-w-96 h-96 text-center flex flex-col justify-center items-center"
                    >
                        <img
                            class="object-fit"
                            src={food.foodPictures[idx].src}
                            alt={food.name}
                        />
                    </div>
                {/if}
            </div>
        {/each}
    {/if}

    <div class="pb-5">
        <div
            class="w-full {isDiscounted
                ? 'bg-red-500'
                : 'bg-red-300'} m-0 text-center py-2 border border-red-200 shadow-md"
        >
            <h5
                class="text-xl font-semibold tracking-tight text-gray-900 uppercase "
            >
                {name}
            </h5>
        </div>
        <div class="px-5 mt-5 max-h-96">
            <div class="h-32 overflow-auto">
                {@html ingredient}
            </div>
            <hr />
            <div class="mt-3">
                <div>
                    {#if availableFoodSizes.length}
                        {#each availableFoodSizes as foodSize}
                            <div class="text-sm font-bold text-gray-900 mb-1">
                                <Button on:click={() => {
                                    toCartClick(food.id, foodSize.id)
                                }}>
                                    <div class="btnGrid">
                                        <span>
                                            {foodSize.name}:
                                        </span>
                                        {#if isDiscounted}
                                            <span class="line-through"
                                                >{food.basePrice *
                                                    foodSize.multiplier} Ft</span
                                            >
                                            <span class="text-red-500"
                                                >{food.discountPrice *
                                                    foodSize.multiplier} Ft</span
                                            >
                                        {:else}
                                            <span>
                                                {food.basePrice *
                                                    foodSize.multiplier} Ft
                                            </span>
                                        {/if}
                                    </div>
                                </Button>
                            </div>
                        {/each}
                    {:else}
                        <div class="text-xl font-bold text-gray-900 mb-1">
                            <span>{food.basePrice} Ft</span>
                        </div>
                    {/if}
                </div>
                <div class="flex justify-between items-center">
                    <slot name="buttons" />
                </div>
            </div>
        </div>
    </div>
</Card>

<style lang="scss">
    .btnGrid {
        display: grid;
        grid-template-columns: 40% 30% 30%;
        gap: 10px;

        span:first-of-type {
            text-align: right;
        }
    }
</style>
