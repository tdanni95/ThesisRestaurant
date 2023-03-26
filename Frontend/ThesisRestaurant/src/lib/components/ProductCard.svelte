<script lang="ts">
    import type { Food } from "$lib/types/classData";
    import { Card } from "flowbite-svelte";
    import { onDestroy, onMount } from "svelte";

    export let food: Food;

    $: name = food.name;
    export let ingredient: string;
    $: foodType = food.foodType.name;

    let isDiscounted = food.discountPrice < food.basePrice;

    let currentImage = 0;
    let to: NodeJS.Timer;
    onMount(() => {
        to = setInterval(() => {
            if (currentImage == food.foodPictures.length - 1) currentImage = 0;
            else currentImage++;
        }, 5000);
    });

    onDestroy(() => {
        clearInterval(to);
    });
</script>

<Card padding="none" size="lg">
    <!-- <img class="p-8 rounded-t-lg" {src} alt={name} /> -->
    <div class="text-center w-full">
        <h1 class="font-bold text-2xl font-xl">
            {foodType || "Choose a foodtype"}
        </h1>
    </div>
    <hr class="my-1" />
    {#if food.foodPictures && food.foodPictures.length}
    <div class="w-full flex  justify-center items-center">
        <div class="w-96 h-96 text-center flex flex-col justify-center items-center">
            <img class="object-contain" src={food.foodPictures[currentImage].src} alt={food.name} />
        </div>   
    </div>
    {/if}

    <div class="pb-5">
        <div
            class="w-full bg-red-300 m-0 text-center py-2 border border-red-200 shadow-md"
        >
            <h5
                class="text-xl font-semibold tracking-tight text-gray-900 uppercase "
            >
                {name}
            </h5>
        </div>
        <div class="px-5 mt-5">
            <p>{@html ingredient}</p>

            <div class="mt-3">
                <span class="text-3xl font-bold text-gray-900">
                    {#if isDiscounted}
                        <span class="line-through">{food.basePrice} Ft</span>
                        <span class="text-red-500">{food.discountPrice} Ft</span
                        >
                    {:else}
                        {food.basePrice} Ft
                    {/if}
                </span>
                <div class="flex justify-between items-center">
                    <slot name="buttons" />
                </div>
            </div>
        </div>
    </div>
</Card>
