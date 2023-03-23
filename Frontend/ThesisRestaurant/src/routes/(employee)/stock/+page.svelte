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
    import { invalidate } from "$app/navigation";
    import Button from "$lib/components/Button.svelte";
    import { json } from "@sveltejs/kit";
    import toastStore from "$lib/stores/toastStore";
    import type { IngredientTypeErrors } from "$lib/types/errors";
    import Swal from "sweetalert2";
    import { each } from "svelte/internal";
    import IngredientTypeForm from "$lib/components/stockForms/IngredientTypeForm.svelte";
    import FoodTypeForm from "$lib/components/stockForms/FoodTypeForm.svelte";
    import IngredientForm from "$lib/components/stockForms/IngredientForm.svelte";
    import FoodSizeForm from "$lib/components/stockForms/FoodSizeForm.svelte";

    let isLoading = false;
    export let data: PageData;

    $: ingredientTypes = data.ingredientTypes as Array<IngredientType>;
    let currentIngredientType: IngredientType = {} as IngredientType;
    let isIngredientTypeEdit: boolean = false;

    $: ingredients = data.ingredients as Array<Ingredient>;
    let currentIngredient: Ingredient = {} as Ingredient;
    let isIngredientEdit: boolean = false;

    $: foodTypes = data.foodTypes as Array<FoodType>;
    let currentFoodType: FoodType = {} as FoodType;
    let isFoodTypeEdit: boolean = false;

    $: foodSizes = data.foodSizes as Array<FoodSize>;
    let currentFoodSize: FoodSize = {} as FoodSize;
    let isFoodSizeEdit: boolean = false;

    const invalidateLoad = () => {
        invalidate("app:stock");
    };

    const doFetch = async (
        url: string,
        method: string,
        body?: {} | undefined
    ) => {
        isLoading = true;
        const response = await fetch(url, {
            method: method,
            body: JSON.stringify(body),
        });
        isLoading = false;
        return response;
    };

    const doDelete = async (id: number, route: string, name: string) => {
        await Swal.fire({
            title: `Are you sure you want to delete the ${name}?`,
            icon: "question",
            showCancelButton: true,
            showLoaderOnConfirm: true,
            preConfirm: async () => {
                const resp = await doFetch(
                    `api/stock?type=${route}&id=${id}`,
                    "DELETE",
                    {}
                );
                const res = await resp.json();
                if (!res.title) {
                    toastStore.success("Deleted successfully", 2000);
                    invalidateLoad();
                } else {
                    toastStore.error(res.title, 2000);
                }
            },
        });
    };

    const doSave = async (
        route: string,
        body: {},
        errorHandler: (res: any) => void
    ) => {
        
        const response = await doFetch(`api/stock?type=${route}`, "POST", body);
        const res = await response.json();
        errorHandler(res);
        if (!res.errors) {
            invalidateLoad();
        }
    };

    const doEdit = async (
        obj: {},
        route: string,
        errorHandler: (res: any) => void
    ) => {
        const response = await doFetch(`api/stock?type=${route}`, "PUT", obj);
        const res = await response.json();
        errorHandler(res);
        if (!res.errors) {
            invalidateLoad();
        }
    };

    const deleteIngredientType = async (id: number) => {
        await doDelete(id, "ingredienttype", "ingredient type");
    };

    const triggerIngredeintTypeEdit = (obj: IngredientType) => {
        isIngredientTypeEdit = true;
        currentIngredientType = JSON.parse(JSON.stringify(obj));
    };

    const deleteFoodType = async (id: number) => {
        await doDelete(id, "foodtype", "food type");
    };
    const triggerFoodTypeEdit = (obj: FoodType) => {
        isFoodTypeEdit = true;
        currentFoodType = JSON.parse(JSON.stringify(obj));;
    };

    const deleteIngredient = async (id:number) =>{
        await doDelete(id, "ingredient", "ingredient");
    }

    const triggerIngredientEdit = (obj:Ingredient) => {
        isIngredientEdit = true;
        currentIngredient = JSON.parse(JSON.stringify(obj));
    }

    const deleteFoodSize = async (id:number) => {
        await doDelete(id, "foodsize", "food size");
    }

    const triggerFoodSizeEdit = async (obj:FoodSize) => {
        isFoodSizeEdit = true
        console.log(obj);
        
        currentFoodSize = JSON.parse(JSON.stringify(obj));
    }
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
    <div class="bg-gray-200 text-gray-500 rounded-3xl shadow-xl w-11/12 py-10">
        <h1 class="font-bold text-3xl text-center text-gray-900">
            Ingredient types
        </h1>
        <div class="md:flex w-full">
            <div class="w-full md:w-6/12 pb-10 px-5 md:px-10">
                <SortTable
                    columns={["name", "minOption", "maxOption"]}
                    ogArray={ingredientTypes}
                    deleteFunc={deleteIngredientType}
                    editFunc={triggerIngredeintTypeEdit}
                />
            </div>
            <div class="w-full md:w-6/12 pb-10 px-5 md:px-10">
                <IngredientTypeForm
                    {doEdit}
                    {doSave}
                    bind:currentIngredientType
                    bind:isIngredientTypeEdit
                    bind:isLoading
                />
            </div>
        </div>
    </div>
</div>

<div class="min-w-screen  flex items-center justify-center px-5 py-5">
    <div
        class="bg-gray-200 h-fit py-10 text-gray-500 rounded-3xl shadow-xl w-11/12"
    >
        <h1 class="font-bold text-3xl text-center text-gray-900">
            Ingredients
        </h1>
        <div class="md:flex w-full">
            <div class="w-full md:w-6/12 pb-10 px-5 md:px-10">
                <SortTable
                    columns={["name", "price", ["ingredientType", "name"]]}
                    ogArray={ingredients}
                    deleteFunc={deleteIngredient}
                    editFunc={triggerIngredientEdit}
                />
            </div>
            <div class="w-full md:w-6/12 pb-10 px-5 md:px-10">
                <IngredientForm 
                {doEdit}
                {doSave}
                bind:isIngredientEdit
                bind:currentIngredient
                bind:isLoading
                bind:ingredientTypeList={ingredientTypes}
                />
            </div>
        </div>
    </div>
</div>

<div class="min-w-screen flex items-center justify-center px-5 py-5">
    <div class="bg-gray-200 text-gray-500 rounded-3xl shadow-xl w-11/12 py-10">
        <h1 class="font-bold text-3xl text-center text-gray-900">Food types</h1>
        <div class="md:flex w-full">
            <div class="w-full md:w-6/12 pb-10 px-5 md:px-10">
                <SortTable
                    columns={["name", "price"]}
                    ogArray={foodTypes}
                    deleteFunc={deleteFoodType}
                    editFunc={triggerFoodTypeEdit}
                />
            </div>
            <div class="w-full md:w-6/12 pb-10 px-5 md:px-10">
                <FoodTypeForm
                    bind:currentFoodType
                    bind:isFoodTypeEdit
                    bind:isLoading
                    {doEdit}
                    {doSave}
                />
            </div>
        </div>
    </div>
</div>

<div class="min-w-screen flex items-center justify-center px-5 py-5">
    <div class="bg-gray-200 text-gray-500 rounded-3xl shadow-xl w-11/12 py-10">
        <h1 class="font-bold text-3xl text-center text-gray-900">Food sizes</h1>
        <div class="md:flex w-full">
            <div class="w-full md:w-6/12 pb-10 px-5 md:px-10">
                <SortTable
                    columns={["name", "multiplier", ["foodType", "name"]]}
                    ogArray={foodSizes}
                    deleteFunc={deleteFoodSize}
                    editFunc={triggerFoodSizeEdit}
                />
            </div>
            <div class="w-full md:w-6/12 pb-10 px-5 md:px-10">
                <FoodSizeForm
                    bind:currentFoodSize
                    bind:isFoodSizeEdit
                    bind:isLoading
                    {doEdit}
                    {doSave}
                    bind:foodTypeList={foodTypes}
                />
            </div>
        </div>
    </div>
</div>
