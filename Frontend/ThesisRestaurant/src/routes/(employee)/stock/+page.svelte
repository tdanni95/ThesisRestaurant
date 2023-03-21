<script lang="ts">
    import { page } from "$app/stores";
    import Input from "$lib/components/Input.svelte";
    import type { Ingredient } from "$lib/types/classData";
    import type { PageData } from "./$types";
    import FaSort from 'svelte-icons/fa/FaSort.svelte'
    import SortTable from "$lib/components/SortTable.svelte";
    let isLoading = false;
    export let data: PageData;

    let ingredients: Array<Ingredient> = data.ingredients;
    let currentIngredient: Ingredient | undefined = undefined;
    

    let tableIngredients = ingredients;
    const doFetch = (
        url: string,
        method: string,
        next: () => void,
        body?: {} | undefined
    ) => {};
    let ingredientRowConfig = ["name", "price", ["ingredientType", "name"]]
    
    const filterIngredientByCol = (value: string) => {
        value = value.toLowerCase()
        tableIngredients = ingredients.filter((i) => {
            let price = i.price.toString().toLowerCase()
            return i.name.toLowerCase().includes(value) || price.includes(value) || i.ingredientType?.name.toLowerCase().includes(value)
        });
    };

    const sortIngredientsByCol = (col: string, eventTarget: EventTarget & HTMLTableCellElement) => {
        let currentOrder = eventTarget.dataset.sort
        let newOrder = "asc"
        let firstReturn = 1
        if(currentOrder && currentOrder == "asc"){
            newOrder = "desc"
            firstReturn = -1
        }
        
        eventTarget.setAttribute("data-sort", newOrder)
        tableIngredients = tableIngredients.sort((a, b) => {
            let aVal = a[col]
            let bVal = b[col]            
            if(col === "type"){
                aVal = a.ingredientType.name
                bVal = b.ingredientType.name
            }
            if(aVal > bVal) return firstReturn
            else if(bVal > aVal) return firstReturn * -1
            return 0
        })
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
admin page vagyok
<!-- {#each ingredients as ingredient}
    <p>{ingredient.name}</p>
{/each} -->


<SortTable columns={["Name", "Price", "Type"]} rowData={tableIngredients} rowConfig={ingredientRowConfig} sortFunc={sortIngredientsByCol} filterFunc={filterIngredientByCol}/>