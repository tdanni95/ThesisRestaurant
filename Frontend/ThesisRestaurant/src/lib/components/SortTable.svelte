<script lang="ts">
    import FaSort from "svelte-icons/fa/FaSort.svelte";
    import type {
        Ingredient,
        IngredientType,
        FoodType,
        FoodSize,
    } from "$lib/types/classData";
    type T = $$Generic<Ingredient | IngredientType | FoodType | FoodSize>;
    export let columns: Array<string | Array<string>>;
    export let ogArray: Array<T>;

    let rowData: Array<T> = ogArray;
    console.log(rowData.length);

    export let needEditAndDelete: boolean = true;

    const getObjectField = (object: T, field: string | Array<string>) => {
        if (Array.isArray(field)) {
            return (object[field[0]] as T)[field[1]];
        }
        return object[field];
    };

    const filterFunc = (value: string) => {
        value = value.toLowerCase();
        rowData = ogArray.filter((i) => {
            let shouldShow = false;
            for (const config of columns) {
                let val = getObjectField(i, config);
                shouldShow =
                    shouldShow || val.toString().toLowerCase().includes(value);
            }
            return shouldShow;
        });
    };

    const mySortFunc = (
        col: string | Array<string>,
        eventTarget: EventTarget & HTMLTableCellElement
    ) => {
        let currentOrder = eventTarget.dataset.sort;
        let newOrder = "asc";
        let firstReturn = 1;
        if (currentOrder && currentOrder == "asc") {
            newOrder = "desc";
            firstReturn = -1;
        }
        eventTarget.setAttribute("data-sort", newOrder);
        rowData = rowData.sort((a, b) => {
            let aVal = getObjectField(a, col);
            let bVal = getObjectField(b, col);

            if (aVal > bVal) return firstReturn;
            else if (bVal > aVal) return firstReturn * -1;
            return 0;
        });
    };
</script>

<div class="relative">
    <label for="table-search" class="sr-only">Search</label>
    <div class="relative mt-1 mb-3">
        <div
            class="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none"
        >
            <svg
                class="w-5 h-5 text-gray-500 "
                aria-hidden="true"
                fill="currentColor"
                viewBox="0 0 20 20"
                xmlns="http://www.w3.org/2000/svg"
                ><path
                    fill-rule="evenodd"
                    d="M8 4a4 4 0 100 8 4 4 0 000-8zM2 8a6 6 0 1110.89 3.476l4.817 4.817a1 1 0 01-1.414 1.414l-4.816-4.816A6 6 0 012 8z"
                    clip-rule="evenodd"
                /></svg
            >
        </div>
        <input
            type="text"
            id="table-search"
            class="block p-2 pl-10 text-sm text-gray-900 border border-gray-300 rounded-lg w-80 bg-gray-50 focus:ring-blue-500 focus:border-blue-500"
            placeholder="Search for items"
            on:keyup={(e) => filterFunc(e.currentTarget.value)}
        />
    </div>
    <div class="overflow-y-auto">
        <table class="w-full text-sm text-left text-gray-500 ">
            <thead class="text-xs text-gray-700 uppercase bg-gray-50 ">
                <tr>
                    {#if needEditAndDelete}
                        <th scope="col" class="px-6 py-3"> Actions </th>
                    {/if}
                    {#each columns as col}
                        <th
                            scope="col"
                            class="px-6 py-3 cursor-pointer opacity-70 hover:opacity-100 tableHead"
                            data-sort="asc"
                            on:click={(e) => {
                                mySortFunc(col, e.currentTarget);
                            }}
                        >
                            <div class="h-5 ml-2">
                                {Array.isArray(col) ? col[1] : col}
                                <FaSort />
                            </div>
                        </th>
                    {/each}
                </tr>
            </thead>
            <tbody>
                {#if rowData.length === 0}
                    <tr class="bg-white border-b text-center">
                        <td class="px-6 py-4 text-3xl" colspan="4">No result</td
                        >
                    </tr>
                {:else}
                    {#each rowData as data}
                        <tr class="bg-white border-b ">
                            {#if needEditAndDelete}
                                <th
                                    scope="row"
                                    class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap"
                                >
                                    <p
                                        class="text-blue-500 cursor-pointer hover:text-blue-700"
                                    >
                                        Edit
                                    </p>
                                    <p
                                        class="text-red-500 cursor-pointer hover:text-red-700"
                                    >
                                        Delete
                                    </p>
                                </th>
                            {/if}
                            {#each columns as config}
                                <td class="px-6 py-4">
                                    {getObjectField(data, config)}
                                </td>
                            {/each}
                        </tr>
                    {/each}
                {/if}
            </tbody>
        </table>
    </div>
</div>

<style lang="scss">
    div {
        max-height: 40vh;
    }
    thead {
        position: sticky;
        top: 0;
    }
    p {
        transition: color 0.4s ease;
    }
    th {
        transition: opacity 0.4s ease;
    }
    .tableHead {
        div {
            display: grid;
            align-items: center;
            justify-content: center;
            grid-template-columns: repeat(2, 1fr);
        }
    }
</style>
