<script lang="ts">
    import Input from "./Input.svelte";
    import type { AddressError } from "$lib/types/registerErrors";
    export let errors: AddressError | undefined = undefined;
    export let addressData: Address = {
        city: "",
        street: "",
        houseNumber: "",
    } as Address;

    import MdLocationCity from "svelte-icons/md/MdLocationCity.svelte";
    import MdLocationOn from "svelte-icons/md/MdLocationOn.svelte";
    import GiMushroomHouse from "svelte-icons/gi/GiMushroomHouse.svelte";
    import type { Address } from "$lib/types/address";

</script>

<div class="flex -mx-3">
    <h1>
        {addressData.zipCode}, {addressData.city}
        {addressData.street}
        {addressData.houseNumber}
    </h1>
</div>
<div class="flex -mx-3">
    <Input
        bind:value={addressData.zipCode}
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
        bind:value={addressData.city}
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
        bind:value={addressData.street}
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
        bind:value={addressData.houseNumber}
        inputName="houseNumber[]"
        label="House Number"
        parentWidth="w-full"
        error={errors?.HouseNumber}
    >
        <GiMushroomHouse slot="icon" />
    </Input>
</div>
<div class="flex -ml-6 mr-2">
    <slot />
</div>
