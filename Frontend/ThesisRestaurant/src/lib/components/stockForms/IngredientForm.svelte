<script lang="ts">
    import type { Ingredient, IngredientType } from "$lib/types/classData";
    import type {  IngredientErrors } from "$lib/types/errors";
    import Input from "$lib/components/Input.svelte";
    import Button from "$lib/components/Button.svelte";
    import toastStore from "$lib/stores/toastStore";
    import Select from "$lib/components/Select.svelte";

    export let currentIngredient: Ingredient;
    export let isIngredientEdit: boolean;
    export let isLoading: boolean;
    export let ingredientTypeList: Array<IngredientType>

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

    const ingerdientErrors: IngredientErrors = {
        Name: "",
        Price: "",
        Type: "",
    };

    const cancelIngredientEdit = () => {
        isIngredientEdit = false;
        currentIngredient.name = "";
        currentIngredient.price = 0;
        currentIngredient.id = 0;
        currentIngredient.ingredientType = {} as IngredientType;
    };

    const handleIngredientErrors = (res: any) => {
        console.log(res);
        
        if (res.title) {
            toastStore.error(res.title, 2000);
        }
        if (res.errors) {
            for (const err in res.errors) {
                if(err == "IngredientTypeId"){
                    console.log("azám");
                    
                    ingerdientErrors.Type = res.errors[err]
                }else{
                    ingerdientErrors[err] = res.errors[err];
                }
            }
        } else if (!res.title) {
            toastStore.success("Saved successfully", 2000);
        }
        console.log(ingerdientErrors);
        
    };

    const handleSelection = (id:number) => {
        const myType = ingredientTypeList.filter((it) => it.id == id)
        if(myType && myType[0]){
            currentIngredient.ingredientType = myType[0]
        }else{
            currentIngredient.ingredientType = {} as IngredientType;
        }
    }
</script>

<div class="flex -mx-3 mt-5 justify-center">
    <h1 class="font-bold text-3xl text-gray-900">Ingredient data</h1>
</div>
<div class="flex -mx-3">
    <Input
        label="Name"
        inputName="name"
        bind:value={currentIngredient.name}
        parentWidth="w-full"
        error={ingerdientErrors.Name}
    />
</div>
<div class="flex -mx-3">
    <Input
        label="Price"
        inputName="price"
        bind:value={currentIngredient.price}
        parentWidth="w-full"
        regex={/[^0-9.]/g}
        error={ingerdientErrors.Price}
    />
</div>
<div class="flex -mx-3">
    <Select
    inputName={"ingredienttype"}
    label={"Ingredient type"}
    bind:data={ingredientTypeList}
    value={currentIngredient.ingredientType?.id}
    parentWidth={"w-full"}
    error={ingerdientErrors.Type}
    on:selectionchange={(e) => {
        handleSelection(+e.detail.id)
    }}
/>
</div>
{#if !isIngredientEdit}
    <div class="flex mt-10">
        <div class="w-full px-3 mb-5 max">
            <Button disabled={isLoading}
                on:click={() =>
                    doSave(
                        "ingredient",
                        {
                            name: currentIngredient.name,
                            price: currentIngredient.price,
                            ingredientTypeId: currentIngredient.ingredientType ? currentIngredient.ingredientType.id : 0
                        },
                        handleIngredientErrors
                    )}>Save</Button
            >
        </div>
    </div>
{:else}
    <div class="flex mt-10">
        <div class="w-full px-3 mb-5">
            <Button disabled={isLoading}
                btnClass="btn-primary"
                on:click={() =>
                    doEdit(
                        {
                            id: currentIngredient.id,
                            name: currentIngredient.name,
                            price: currentIngredient.price,
                            ingredientTypeId: currentIngredient.ingredientType ? currentIngredient.ingredientType.id : 0
                        },
                        "ingredient",
                        handleIngredientErrors
                    )}>Update</Button
            >
        </div>
        <div class="w-full px-3 mb-5 max">
            <Button disabled={isLoading} btnClass="btn-error" on:click={cancelIngredientEdit}
                >Cancel edit</Button
            >
        </div>
    </div>
{/if}