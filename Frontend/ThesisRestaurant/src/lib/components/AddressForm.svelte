<script lang="ts">
    import Input from "./Input.svelte";
    import type { AddressError } from "$lib/types/registerErrors";
    export let errors: AddressError | undefined = undefined;

    import MdLocationCity from "svelte-icons/md/MdLocationCity.svelte";
    import MdLocationOn from "svelte-icons/md/MdLocationOn.svelte";
    import GiMushroomHouse from "svelte-icons/gi/GiMushroomHouse.svelte";
    import type { Address } from "$lib/types/address";
    
    let address: Address = {
        city: "",
        street: "",
        houseNumber: "",
    } as Address;
</script>

<div class="flex -mx-3">
    <h1>
        {address.zipCode}, {address.city}
        {address.street}
        {address.houseNumber}
    </h1>
</div>
<div class="flex -mx-3">
    <Input
        bind:value={address.zipCode}
        inputName="zipCode[]"
        label="ZipCode"
        parentWidth="w-full"
        error={errors?.ZipCode}
        required={true}
        regex={/[^0-9]/g}
    >
        <MdLocationOn slot="icon" />
    </Input>
</div>
<div class="flex -mx-3">
    <Input
        bind:value={address.city}
        inputName="city[]"
        label="City"
        parentWidth="w-full"
        error={errors?.City}
    >
        <MdLocationCity slot="icon" />
    </Input>
</div>
<div class="flex -mx-3">
    <Input
        bind:value={address.street}
        inputName="street[]"
        label="Street"
        parentWidth="w-full"
        error={errors?.Street}
    >
        <MdLocationOn slot="icon" />
    </Input>
</div>
<div class="flex -mx-3">
    <Input
        bind:value={address.houseNumber}
        inputName="houseNumber[]"
        label="House Number"
        parentWidth="w-full"
        error={errors?.HouseNumber}
    >
        <GiMushroomHouse slot="icon" />
    </Input>
</div>

