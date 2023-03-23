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
    let isLoading = false;
    export let data: PageData;

    $: ingredientTypes = data.ingredientTypes as Array<IngredientType>;
    let currentIngredientType: IngredientType = {} as IngredientType;
    let isIngredientTypeEdit: boolean = false;
    const ingredientTypeErrors: IngredientTypeErrors = {
        Name: "",
        MinOption: "",
        MaxOption: "",
    };

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
        const response = await fetch(url, {
            method: method,
            body: JSON.stringify(body),
        });
        return response;
    };

    const handleIngredientTypeErrors = (res:any) => {
        if (res.title) {
            toastStore.error(res.title, 2000);
        }
        if (res.errors) {
            for (const err in res.errors) {
                ingredientTypeErrors[err] = res.errors[err];
            }
        } else if(!res.title){
            toastStore.success("Saved successfully", 2000);
            invalidateLoad();
        }
    }

    const saveIngredientType = async () => {
        const response = await doFetch(
            `api/stock?type=ingredienttype`,
            "POST",
            {
                name: currentIngredientType.name,
                minOption: currentIngredientType.minOption,
                maxOption: currentIngredientType.maxOption,
            }
        );
        const res = await response.json();
        handleIngredientTypeErrors(res)
    };

    const editIngredientType = async () => {
        const response = await doFetch(
            `api/stock?type=ingredienttype`,
            "PUT",
            currentIngredientType
        );
        const res = await response.json();
        handleIngredientTypeErrors(res)
    };

    const deleteIngredientType = async (id:number) => {
        await Swal.fire({
            title: "Are you sure you want to delete the ingredient type?",
            icon: "question",
            showCancelButton: true,
            showLoaderOnConfirm: true,
            preConfirm: async () => {
                const resp = await doFetch(`api/stock?type=ingredienttype&id=${id}`, "DELETE", {});
                const res = await resp.json()
                console.log(res);
                
                if(!res.title) {
                    toastStore.success("Deleted successfully", 2000)
                    invalidateLoad()
                }
                else{
                    toastStore.error(res.title, 2000)
                }
            },
        });
    }

    const triggerIngredeintTypeEdit = (obj:IngredientType) => {
        isIngredientTypeEdit = true
        currentIngredientType = obj
    }

    const cancelIngredientTypeEdit = () => {
        isIngredientTypeEdit = false
        currentIngredientType.name = ""
        currentIngredientType.minOption = 0
        currentIngredientType.maxOption = 0
        currentIngredientType.id = 0
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
                <div class="flex -mx-3 mt-5 justify-center">
                    <h1 class="font-bold text-3xl text-gray-900">Ingredient type data</h1>
                </div>
                <div class="flex -mx-3">
                    <Input
                        label="Name"
                        inputName="name"
                        bind:value={currentIngredientType.name}
                        parentWidth="w-full"
                        error={ingredientTypeErrors.Name}
                    />
                </div>
                <div class="flex -mx-3">
                    <Input
                        label="Min option"
                        inputName="minOption"
                        bind:value={currentIngredientType.minOption}
                        parentWidth="w-full"
                        regex={/[^0-9]/g}
                        error={ingredientTypeErrors.MinOption}
                    />
                </div>
                <div class="flex -mx-3">
                    <Input
                        label="Max option"
                        inputName="maxOption"
                        bind:value={currentIngredientType.maxOption}
                        parentWidth="w-full"
                        regex={/[^0-9]/g}
                        error={ingredientTypeErrors.MaxOption}
                    />
                </div>
                {#if !isIngredientTypeEdit}
                    <Button on:click={saveIngredientType}>Save</Button>
                {:else}
                    <div class="flex mt-10">
                        <div class="w-full px-3 mb-5 max">
                            <Button btnClass="btn-primary" on:click={editIngredientType}>Update</Button>
                        </div>
                        <div class="w-full px-3 mb-5 max">
                            <Button btnClass="btn-error" on:click={cancelIngredientTypeEdit}>Cancel edit</Button>
                        </div>
                    </div>
                {/if}
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
                <SortTable columns={["name", "price"]} ogArray={foodTypes} />
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
                />
            </div>
        </div>
    </div>
</div>
