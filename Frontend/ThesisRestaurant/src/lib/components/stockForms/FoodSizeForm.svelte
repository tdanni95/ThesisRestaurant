<script lang="ts">
    import type { FoodSize, FoodType } from "$lib/types/classData";
    import type { FoodSizeErrors, IngredientErrors } from "$lib/types/errors";
    import Input from "$lib/components/Input.svelte";
    import Button from "$lib/components/Button.svelte";
    import toastStore from "$lib/stores/toastStore";
    import Select from "$lib/components/Select.svelte";

    export let currentFoodSize: FoodSize;
    export let isFoodSizeEdit: boolean;
    export let isLoading: boolean;
    export let foodTypeList: Array<FoodType>;

    export let doSave: (
        route: string,
        body: {},
        errorHandler: (res: any) => void
    ) => void;

    export let doEdit: (
        obj: {},
        route: string,
        errorHandler: (res: any) => void
    ) => void;

    const foodSizeErrors: FoodSizeErrors = {
        Name: "",
        Multiplier: "",
        Type: "",
    };

    const cancelFoodSizeEdit = () => {
        isFoodSizeEdit = false;
        currentFoodSize.name = "";
        currentFoodSize.multiplier = 0;
        currentFoodSize.id = 0;
        currentFoodSize.foodType = {} as FoodType;
    };

    const handleFoodSizeErrors = (res: any) => {
        if (res.title) {
            toastStore.error(res.title, 2000);
        }
        if (res.errors) {
            for (const err in res.errors) {
                if (err == "foodTypeId") {
                    foodSizeErrors.Type = res.errors[err];
                } else {
                    foodSizeErrors[err] = res.errors[err];
                }
            }
        } else if (!res.title) {
            toastStore.success("Saved successfully", 2000);
        }
        console.log(foodSizeErrors);
    };

    const handleSelection = (id: number) => {
        const myType = foodTypeList.filter((it) => it.id == id);
        if (myType && myType[0]) {
            currentFoodSize.foodType = myType[0];
        } else {
            currentFoodSize.foodType = {} as FoodType;
        }
    };
</script>

<div class="flex -mx-3 mt-5 justify-center">
    <h1 class="font-bold text-3xl text-gray-900">Ingredient data</h1>
</div>
<div class="flex -mx-3">
    <Input
        label="Name"
        inputName="name"
        bind:value={currentFoodSize.name}
        parentWidth="w-full"
        error={foodSizeErrors.Name}
    />
</div>
<div class="flex -mx-3">
    <Input
        label="Multiplier"
        inputName="multiplier"
        bind:value={currentFoodSize.multiplier}
        parentWidth="w-full"
        regex={/[^0-9.]/g}
        error={foodSizeErrors.Multiplier}
    />
</div>
<div class="flex -mx-3">
    <Select
        inputName={"foodtype"}
        label={"Food type"}
        bind:data={foodTypeList}
        value={currentFoodSize.foodType ? currentFoodSize.foodType.id : ""}
        parentWidth={"w-full"}
        error={foodSizeErrors.Type}
        on:selectionchange={(e) => {
            handleSelection(+e.detail.id);
        }}
    />
</div>
{#if !isFoodSizeEdit}
    <div class="flex mt-10">
        <div class="w-full px-3 mb-5 max">
            <Button
                disabled={isLoading}
                on:click={() =>
                    doSave(
                        "foodsize",
                        {
                            name: currentFoodSize.name,
                            multiplier: currentFoodSize.multiplier,
                            foodTypeId: currentFoodSize.foodType
                                ? currentFoodSize.foodType.id
                                : 0,
                        },
                        handleFoodSizeErrors
                    )}>Save</Button
            >
        </div>
    </div>
{:else}
    <div class="flex mt-10">
        <div class="w-full px-3 mb-5 max">
            <Button
                disabled={isLoading}
                btnClass="btn-primary"
                on:click={() =>
                    doEdit(
                        {
                            id: currentFoodSize.id,
                            name: currentFoodSize.name,
                            multiplier: currentFoodSize.multiplier,
                            foodTypeId: currentFoodSize.foodType
                                ? currentFoodSize.foodType.id
                                : 0,
                        },
                        "foodsize",
                        handleFoodSizeErrors
                    )}>Update</Button
            >
        </div>
        <div class="w-full px-3 mb-5 max">
            <Button
                disabled={isLoading}
                btnClass="btn-error"
                on:click={cancelFoodSizeEdit}>Cancel edit</Button
            >
        </div>
    </div>
{/if}
