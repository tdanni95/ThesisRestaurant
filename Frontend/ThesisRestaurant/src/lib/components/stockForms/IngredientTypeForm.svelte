<script lang="ts">
    import type { IngredientType } from "$lib/types/classData";
    import type { IngredientTypeErrors } from "$lib/types/errors";
    import Input from "$lib/components/Input.svelte";
    import Button from "$lib/components/Button.svelte";
    import toastStore from "$lib/stores/toastStore";

    export let currentIngredientType: IngredientType;
    export let isIngredientTypeEdit: boolean;
    export let isLoading:boolean;

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

    const ingredientTypeErrors: IngredientTypeErrors = {
        Name: "",
        MinOption: "",
        MaxOption: "",
    };

    const cancelIngredientTypeEdit = () => {
        isIngredientTypeEdit = false;
        currentIngredientType.name = "";
        currentIngredientType.minOption = 0;
        currentIngredientType.maxOption = 0;
        currentIngredientType.id = 0;
    };

    const handleIngredientTypeErrors = (res: any) => {
        if (res.title) {
            toastStore.error(res.title, 2000);
        }
        if (res.errors) {
            for (const err in res.errors) {
                ingredientTypeErrors[err] = res.errors[err];
            }
        } else if (!res.title) {
            toastStore.success("Saved successfully", 2000);
        }
    };
</script>

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
    <div class="flex mt-10">
        <div class="w-full px-3 mb-5 max">
            <Button disabled={isLoading}
                on:click={() =>
                    doSave(
                        "ingredienttype",
                        {
                            name: currentIngredientType.name,
                            minOption: currentIngredientType.minOption,
                            maxOption: currentIngredientType.maxOption,
                        },
                        handleIngredientTypeErrors
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
                        currentIngredientType,
                        "ingredienttype",
                        handleIngredientTypeErrors
                    )}>Update</Button
            >
        </div>
        <div class="w-full px-3 mb-5 max">
            <Button disabled={isLoading} btnClass="btn-error" on:click={cancelIngredientTypeEdit}
                >Cancel edit</Button
            >
        </div>
    </div>
{/if}
