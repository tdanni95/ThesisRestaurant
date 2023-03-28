<script lang="ts">
    import type { Food } from "$lib/types/classData";
    import { Card } from "flowbite-svelte";
    import { onDestroy, onMount } from "svelte";
    import {  slide } from "svelte/transition";

    export let food: Food;

    $: name = food.name;
    export let ingredient: string;
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
            console.log(idx, food.foodPictures[idx]);

            currentImage = food.foodPictures[idx].id;
        }, 3000);
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
        {#each food.foodPictures as picture (picture.id)}
            <div class="w-full flex  justify-center items-center">
                {#if currentImage === picture.id}
                    <div
                        transition:slide
                        class="overflow-hidden w-96 h-96 max-h-96 text-center flex flex-col justify-center items-center"
                    >
                        <img
                            class="object-contain"
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
