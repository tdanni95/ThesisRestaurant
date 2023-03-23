<script lang="ts">
    import { createEventDispatcher } from "svelte";
    import type {
        Ingredient,
        IngredientType,
        FoodType,
        FoodSize,
    } from "$lib/types/classData";
    type T = $$Generic<Ingredient | IngredientType | FoodType | FoodSize>;

    const dispatch = createEventDispatcher();

    export let parentWidth: string = "w-1/2";
    export let label: string;
    export let inputName: string;
    export let required: boolean = true;
    export let value: string | number = "";

    export let error: string | undefined = undefined;

    export let data: Array<T>;

    const handleChange = (id: string) => {
        dispatch("selectionchange", {
            id: id,
        });
    };

    $: hasError = Boolean(error);
</script>

<div class="{parentWidth} px-3 mb-2">
    <label for={inputName} class="text-xs font-semibold px-1">{label}</label>
    <div class="flex">
        <div
            class="w-6 z-10 pr-1 text-center pointer-events-none flex items-right justify-left"
        >
            <slot name="icon" />
        </div>
        <select
            class="w-full -ml-10 pl-10 pr-3 py-2 rounded-lg border-2 outline-none focus:border-indigo-50 {hasError
                ? 'border-red-500'
                : 'border-gray-200'}"
            name={inputName}
            {required}
            placeholder={label}
            {value}
            on:change={(e) => handleChange(e.currentTarget.value)}
        >
            <option value="">Please choose</option>
            {#each data as item}
                <option value={item.id}>{item.name}</option>
            {/each}
        </select>
    </div>
        <div class="h-3">
            {#if error && hasError}
                <span
                    class="flex items-center font-medium tracking-wide text-red-500 text-xs"
                >
                    {error}
                </span>
            {/if}
        </div>

</div>
