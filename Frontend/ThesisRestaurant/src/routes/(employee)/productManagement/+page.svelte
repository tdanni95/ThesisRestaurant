<script lang="ts">
    import { page } from "$app/stores";
    import Input from "$lib/components/Input.svelte";
    import type {
        FoodType,
        Ingredient,
        Food,
        IngredientType,
        FoodPicture,
        FoodPrice,
    } from "$lib/types/classData";
    import type { PageData } from "./$types";
    import SortTable from "$lib/components/SortTable.svelte";

    import type { SortTableFormatters } from "$lib/types/sortTableFormatters";
    import ProductCard from "$lib/components/ProductCard.svelte";
    import FoodForm from "$lib/components/FoodForm.svelte";
    import Button from "$lib/components/Button.svelte";
    import { invalidate } from "$app/navigation";
    import toastStore from "$lib/stores/toastStore";
    import type { FoodErrors } from "$lib/types/errors";
    import { ingredientFormatter } from "$lib/helpers/ingredientFormatter";
    import Swal from "sweetalert2";

    const baseBictures: Array<FoodPicture> = [{ id: 0, src: "pizza.png" }];

    let productDiv: HTMLDivElement;

    let isLoading = false;
    export let data: PageData;

    $: foods = data.foods as Array<Food>;

    let currentFood = {
        name: "",
        basePrice: 0,
        discountPrice: Number.MAX_SAFE_INTEGER,
        ingredients: [] as Array<Ingredient>,
        foodType: {} as FoodType,
        foodPictures: baseBictures,
    } as Food;

    let isFoodEdit = false;

    $: ingredientTypes = data.ingredientTypes as Array<IngredientType>;
    $: ingredients = data.ingredients as Array<Ingredient>;
    $: foodTypes = data.foodTypes as Array<FoodType>;

    $: availableFoodSizes = data.foodSizes.filter(
        (fs) => fs.foodType.id === currentFood.foodType.id
    );

    const editFood = (obj: Food) => {
        currentFood = JSON.parse(JSON.stringify(obj));
        if (!currentFood.foodPictures.length) {
            currentFood.foodPictures = baseBictures;
        }
        isFoodEdit = true;
        productDiv.scrollIntoView({ behavior: "smooth" });
    };

    const deleteFood = async (id: number) => {
        const f = foods.find((f) => f.id == id);
        if (!f) {
            toastStore.error("Food not found", 2000);
            return;
        }
        await Swal.fire({
            title: `Are you sure you want to delete the food ${f.name}?`,
            icon: "question",
            showCancelButton: true,
            showLoaderOnConfirm: true,
            preConfirm: async () => {
                const resp = await fetch(`api/food?id=${id}`, {
                    method: "DELETE",
                });
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

    const foodIngredientsFormatter = (food: Food) => {
        return ingredientFormatter(food.ingredients);
    };

    const Formatter: SortTableFormatters<Food> = {
        name: "ingredients",
        callBack: foodIngredientsFormatter,
    };

    const priceFormatter = (food: Food) => {
        return Math.min(food.basePrice, food.discountPrice).toString();
    };

    const PriceFormatter: SortTableFormatters<Food> = {
        name: "price",
        callBack: priceFormatter,
    };

    const pictureFormatter = (picture:FoodPicture) => {
        let splitted =  picture.src.split('/')
        let srcText = splitted[splitted.length -1]
        return `<a class='text-blue-500 hover:text-blue-700 transition-colors' href='${picture.src}' target='_blank'>${srcText}</a>`
    }

    const PictureFormatter:SortTableFormatters<FoodPicture> = {
        name: "src",
        callBack: pictureFormatter
    }

    const invalidateLoad = () => {
        invalidate("app:productManagement");
    };

    const createFood = async () => {
        // string Name, double BasePrice, int FoodTypeId, List<int> IngredientIds
        const json = {
            name: currentFood.name,
            basePrice: currentFood.basePrice,
            foodTypeId: currentFood.foodType.id,
            IngredientIds: currentFood.ingredients.map((i) => i.id),
        };
        const response = await fetch(`api/food`, {
            body: JSON.stringify(json),
            method: "POST",
        });
        const res = await response.json();

        if (res.errors) {
            handleFoodErrors(res);
        } else {
            toastStore.success("Food created successfully", 2000);
            resetCurrentFood();
            invalidateLoad();
        }
    };
    let errors: FoodErrors = {} as FoodErrors;
    $: countErrors = [] as Array<string>;
    const handleFoodErrors = (res: any) => {
        toastStore.error(res.title, 2000);
        for (const err in res.errors) {
            if (err.includes("CountError")) {
                countErrors.push(res.errors[err]);
            }
            errors[err] = res.errors[err];
        }
    };

    const updateFood = async () => {
        const json = {
            name: currentFood.name,
            basePrice: currentFood.basePrice,
            foodTypeId: currentFood.foodType.id,
            IngredientIds: currentFood.ingredients.map((i) => i.id),
        };
        const response = await fetch(`api/food?id=${currentFood.id}`, {
            body: JSON.stringify(json),
            method: "PUT",
        });

        const res = await response.json();

        if (res.errors) {
            handleFoodErrors(res);
        } else {
            toastStore.success("Food updated successfully", 2000);
            resetCurrentFood();
            invalidateLoad();
        }
    };

    const cancelEditing = () => {
        isFoodEdit = false;
        resetCurrentFood();
    };

    let fileInput: HTMLInputElement;

    const sendFiles = async () => {
        let files = fileInput.files;
        if (!files || !files[0]) {
            toastStore.error("Select a picture first!", 2000);
        }
        let fd = new FormData();
        fd.append("file", files![0]);
        let result = await fetch(`api/file?id=${currentFood.id}`, {
            method: "POST",
            body: fd,
        });
        let res = await result.json();

        if (res.msg) {
            toastStore.success("File uploaded successfully", 2000);
            invalidateLoad();
        } else {
            toastStore.error(res.title, 2000);
        }
    };

    const resetCurrentFood = () => {
        currentFood = {
            name: "",
            basePrice: 0,
            discountPrice: Number.MAX_SAFE_INTEGER,
            ingredients: [] as Array<Ingredient>,
            foodType: {} as FoodType,
            foodPictures: baseBictures,
        } as Food;
    };


    async function deletePic(id: number) {
        if(id === 0){
            toastStore.error("Can't delete default picture!", 2000)
            return
        }
        const pic = currentFood.foodPictures.find(fp => fp.id === id)
        if(!pic) return
        await Swal.fire({
            title: "Delete picture?",
            icon: 'question',
            imageUrl: pic.src,
            imageHeight: 200,
            showLoaderOnConfirm: true,
            showCancelButton: true,
            preConfirm: async function() {
                const response = await fetch(`api/file?id=${id}`, {method: "DELETE"})
                const res = await response.json()
                if(res.msg){
                    toastStore.success("File deleted successfully!", 2000)
                    invalidateLoad()
                }else{
                    console.log(res);
                    toastStore.error(res.title, 2000)
                }
            }
        })
    }
</script>

<div class="min-w-screen flex items-center justify-center px-5 py-5">
    <div class="bg-gray-200 text-gray-500 rounded-3xl shadow-xl w-11/12 py-10">
        <h1 class="font-bold text-3xl text-center text-gray-900">Foods</h1>
        <div class="md:flex w-full">
            <div class="w-full pb-10 px-5 md:px-10">
                <SortTable
                    columns={[
                        "name",
                        "price",
                        ["foodType", "name"],
                        "ingredients",
                    ]}
                    formatters={[Formatter, PriceFormatter]}
                    ogArray={foods}
                    editFunc={editFood}
                    deleteFunc={deleteFood}
                />
            </div>
        </div>
    </div>
</div>
<div class="min-w-screen flex items-center justify-center px-5 py-5">
    <div class="bg-gray-200 text-gray-500 rounded-3xl shadow-xl w-11/12 py-10">
        <h1 class="font-bold text-3xl text-center text-gray-900">Foods</h1>
        {#if countErrors.length}
            <h3 class="font-bold text-2xl text-center text-red-500">
                Ingredient errors:
            </h3>
            {#each countErrors as error}
                <p class="font-bold text-xl text-center text-red-500">
                    {error}
                </p>
            {/each}
        {/if}
        <div class="md:flex w-full mt-5" bind:this={productDiv}>
            <div class="w-full md:w-6/12 pb-10 px-5 md:px-10">
                <div class="flex mt-5 mb-5">
                    <h1 class="font-bold text-3xl text-gray-900">
                        Food preview
                    </h1>
                </div>
                <ProductCard
                    {availableFoodSizes}
                    bind:food={currentFood}
                    ingredient={ingredientFormatter(currentFood?.ingredients)}
                />
            </div>
            <div class="w-full md:w-6/12 pb-10 px-5 md:px-10">
                <div class="flex mt-5 mb-5">
                    <h1 class="font-bold text-3xl text-gray-900">Food data</h1>
                </div>
                <FoodForm
                    {errors}
                    bind:currentFood
                    {ingredients}
                    {foodTypes}
                    {ingredientTypes}
                >
                    <div class="mt-5 w-full" slot="buttons">
                        {#if !isFoodEdit}
                            <Button disabled={isLoading} on:click={createFood}
                                >Save</Button
                            >
                        {:else}
                            <div class="flex">
                                <Button
                                    btnClass="btn-primary"
                                    disabled={isLoading}
                                    on:click={updateFood}>Update</Button
                                >
                                <Button
                                    btnClass="btn-error"
                                    disabled={isLoading}
                                    on:click={cancelEditing}
                                    >Cancel editing</Button
                                >
                            </div>
                        {/if}
                    </div>
                </FoodForm>

                {#if isFoodEdit}
                    <h1 class="font-bold text-2xl mt-5">Attach pictures:</h1>
                    <input type="file" bind:this={fileInput} />
                    <Button disabled={isLoading} on:click={sendFiles}
                        >Upload</Button
                    >
                {/if}
            </div>
        </div>
        {#if isFoodEdit}
            <div class="flex px-10 mt-5 mb-5">
                <h1 class="font-bold text-3xl text-gray-900">
                    Uploaded pictures
                </h1>
            </div>
            <div class="md:flex w-full mt-5 justify-center">
                <div class="md:flex w-full">
                    <div class="w-full pb-10 px-5 md:px-10">
                        <SortTable
                            columns={["id", "src"]}
                            formatters={[PictureFormatter]}
                            ogArray={currentFood.foodPictures}
                            editFunc={() => {}}
                            deleteFunc={() => {}}
                        >
                            <th
                                slot="actionButtons"
                                scope="row"
                                class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap"
                            >
                                <!-- svelte-ignore a11y-click-events-have-key-events -->
                                <p
                                    class="text-red-500 cursor-pointer hover:text-red-700 transition-colors"
                                    on:click={(e) => {
                                        const closestRow = e.currentTarget.closest('tr')
                                        if(!closestRow) return
                                        const id = closestRow.dataset.id
                                        if(!id) return
                                        deletePic(+id)
                                    }}
                                >
                                    Delete picture
                                </p>
                            </th>
                        </SortTable>
                    </div>
                </div>
            </div>
        {/if}
    </div>
</div>
