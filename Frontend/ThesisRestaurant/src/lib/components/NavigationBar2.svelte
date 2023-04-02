<script>
    import {
        Navbar,
        NavBrand,
        NavLi,
        NavUl,
        NavHamburger,
        Dropdown,
        DropdownItem,
        Chevron,
        DropdownDivider,
    } from "flowbite-svelte";

    import { page } from "$app/stores";
    import { enhance } from "$app/forms";
    import { fly } from "svelte/transition";
    import cartStore from "$lib/stores/cartStore";
</script>

<Navbar
    color="none"
    navDivClass="text-gray-50 mx-auto flex flex-wrap justify-between items-center"
    navClass="bg-gray-700 px-2 sm:px-4 py-2.5 w-full text-gray-50"
    let:hidden
    let:toggle
>
    <NavBrand href="/">
        <span class="self-center whitespace-nowrap text-xl font-semibold ">
            ThesisRestaurant
        </span>
    </NavBrand>
    <NavHamburger on:click={toggle} />
    <NavUl
        {hidden}
        nonActiveClass="text-gray-50 hover:bg-gray-100 md:hover:bg-transparent md:border-0 
        md:hover:text-gray-50 text-gray-50 md:hover:text-white 
        hover:bg-gray-700 hover:text-white md
        hover:bg-transparent"
    >
        {#if $page.data.user}
            {#if $page.data.user.role !== "AppUser"}
                <NavLi id="employeeMenu" class="cursor-pointer text-gray-50"
                    ><Chevron aligned>Employee</Chevron></NavLi
                >
                <Dropdown triggeredBy="#employeeMenu" class="w-44 z-20">
                    <DropdownItem href="/productManagement"
                        >Product Management</DropdownItem
                    >
                    <DropdownItem href="/stock">Stock</DropdownItem>
                    <DropdownItem href="/discount">Manage discounts</DropdownItem>
                    <DropdownItem href="/orders">View orders</DropdownItem>
                </Dropdown>
            {/if}
            {#if $page.data.user.role === "Admin"}
            <NavLi id="adminMenu" class="cursor-pointer text-gray-50"
                    ><Chevron aligned>Admin</Chevron></NavLi
                >
                <Dropdown triggeredBy="#adminMenu" class="w-44 z-20">
                    <DropdownItem href="/users"
                        >Users</DropdownItem
                    >
                </Dropdown>
            {/if}
            <NavLi id="userMenu">
                <Chevron aligned>
                    {$page.data.user.firstName}
                    {$page.data.user.lastName}</Chevron
                >
            </NavLi>
            <Dropdown triggeredBy="#userMenu" class="z-20">
                <DropdownItem href="/profile">Profile</DropdownItem>
                <DropdownItem href="/customFood">My custom foods</DropdownItem>
                <DropdownItem href="/cart">Shopping cart ({$cartStore.foods.length + $cartStore.customFoods.length} item(s) in cart)</DropdownItem>
                <DropdownItem href="/previousOrders">Previous orders</DropdownItem>
            </Dropdown>
            <NavLi>
                <form method="post" action="/login?/logout" use:enhance>
                    <button class="text-gray-50">Logout</button>
                </form>
            </NavLi>
        {:else}
            <NavLi href="/login">Login</NavLi>
        {/if}
    </NavUl>
</Navbar>
