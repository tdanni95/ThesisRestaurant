<script lang="ts">
    import type { FoodType } from "$lib/types/classData";
    import type { FoodTypeErrors, IngredientTypeErrors } from "$lib/types/errors";
    import Input from "$lib/components/Input.svelte";
    import Button from "$lib/components/Button.svelte";
    import toastStore from "$lib/stores/toastStore";

    export let currentFoodType: FoodType;
    export let isFoodTypeEdit: boolean;
    export let isLoading: boolean;

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

    const foodTypeErrors: FoodTypeErrors = {
        Name: "",
        Price: ""
    };

    const cancelFoodTypeEdit = () => {
        isFoodTypeEdit = false;
        currentFoodType.name = "";
        currentFoodType.price = 0
        currentFoodType.id = 0;
    };

    const handleFoodTypeErrors = (res: any) => {
        if (res.title) {
            toastStore.error(res.title, 2000);
        }
        if (res.errors) {
            for (const err in res.errors) {
                foodTypeErrors[err] = res.errors[err];
            }
        } else if (!res.title) {
            toastStore.success("Saved successfully", 2000);
        }
    };
</script>

<div class="flex -mx-3 mt-5 justify-center">
    <h1 class="font-bold text-3xl text-gray-900">Food type data</h1>
</div>
<div class="flex -mx-3">
    <Input
        label="Name"
        inputName="name"
        bind:value={currentFoodType.name}
        parentWidth="w-full"
        error={foodTypeErrors.Name}
    />
</div>
<div class="flex -mx-3">
    <Input
        label="Price"
        inputName="price"
        bind:value={currentFoodType.price}
        parentWidth="w-full"
        regex={/[^0-9.]/g}
        error={foodTypeErrors.Price}
    />
</div>
{#if !isFoodTypeEdit}
    <div class="flex mt-10">
        <div class="w-full px-3 mb-5 max">
            <Button disabled={isLoading}
                on:click={() =>
                    doSave(
                        "foodtype",
                        {
                            name: currentFoodType.name,
                            price: currentFoodType.price,
                        },
                        handleFoodTypeErrors
                    )}>Save</Button
            >
        </div>
    </div>
{:else}
    <div class="flex mt-10">
        <div class="w-full px-3 mb-5 max">
            <Button disabled={isLoading}
                btnClass="btn-primary"
                on:click={() =>
                    doEdit(
                        currentFoodType,
                        "foodtype",
                        handleFoodTypeErrors
                    )}>Update</Button
            >
        </div>
        <div class="w-full px-3 mb-5 max">
            <Button disabled={isLoading} btnClass="btn-error" on:click={cancelFoodTypeEdit}
                >Cancel edit</Button
            >
        </div>
    </div>
{/if}