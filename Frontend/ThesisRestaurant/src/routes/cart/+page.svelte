<script lang="ts">
    import cartStore from "$lib/stores/cartStore";
    import { flip } from "svelte/animate";
    import { fade } from "svelte/transition";
    import { page } from "$app/stores";
    import type { UserData } from "$lib/types/userData";

    let userData = $page.data.userData as UserData

    let selectedAddressId = userData.addresses[0].id
    

</script>

this is your cart ❤
<br />
Your foods 🎉
<table class="w-full text-sm text-left text-gray-500 dark:text-gray-400">
    <tr>
        <th />
        <th>id</th>
        <th>foodsizeid</th>
        <th>additionalingredients</th>
    </tr>
    {#if $cartStore.foods.length}
        {#each $cartStore.foods as cartFood (cartFood.guid)}
            <tr out:fade animate:flip={{ duration: 300 }}>
                <!-- svelte-ignore a11y-click-events-have-key-events -->
                <td
                    class="text-red-500 hover:text-red-700 transition-colors cursor-pointer"
                    on:click={() => {
                        cartStore.deleteItem(cartFood.guid);
                    }}>Delete</td
                >
                <td>{cartFood.id}</td>
                <td>{cartFood.foodSizeId}</td>
                <td>{cartFood.additionalIngredients.join(",")}</td>
            </tr>
        {/each}
    {:else}
        <td class="text-center text-2xl" colspan="4">No result</td>
    {/if}
</table>
Your custom foods 🎉
<table class="w-full text-sm text-left text-gray-500 dark:text-gray-400">
    <tr>
        <th />
        <th>id</th>
    </tr>

    {#if $cartStore.customFoods.length}
        {#each $cartStore.customFoods as cartCustomFood (cartCustomFood.guid)}
            <tr out:fade animate:flip={{ duration: 300 }}> 
                <!-- svelte-ignore a11y-click-events-have-key-events -->
                <td
                    class="text-red-500 hover:text-red-700 transition-colors cursor-pointer"
                    on:click={() => {
                        cartStore.deleteItem(cartCustomFood.guid, true);
                    }}>Delete</td
                >
                <td>{cartCustomFood.id}</td>
            </tr>
        {/each}
    {:else}
        <td class="text-center text-2xl" colspan="2">No result</td>
    {/if}
</table>

<div class="flex justify-center w-8/12 mx-auto">
    {#each userData.addresses as address (address.id)}
        <!-- svelte-ignore a11y-click-events-have-key-events -->
        <div class="addressCard" on:click={() => selectedAddressId = address.id} class:selected={address.id === selectedAddressId}>
            {address.zipCode}, {address.city} {address.street} {address.houseNumber}
        </div>    
    {/each}
</div>

<style lang="scss">
    .addressCard{
        display: grid;
        border: 1px solid black;
        border-radius: 10px;
        margin: 10px;
        padding: 20px;
        transition: background-color 0.3s ease-in-out;
        cursor: pointer;
        &.selected{
            background-color: greenyellow;
        }

        &:hover:not(.selected){
            background-color: lightblue;
        }
    }
</style>