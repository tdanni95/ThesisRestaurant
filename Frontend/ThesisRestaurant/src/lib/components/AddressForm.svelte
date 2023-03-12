<script lang="ts">
    import Input from "./Input.svelte";
    import { setContext } from "svelte";
    import { writable } from "svelte/store";
    import type { AddressError } from "$lib/types/registerErrors";
    export let idx: string | number;
    export let errors: AddressError | undefined = undefined;

    import MdLocationCity from "svelte-icons/md/MdLocationCity.svelte";
    import MdLocationOn from "svelte-icons/md/MdLocationOn.svelte";
    import GiMushroomHouse from "svelte-icons/gi/GiMushroomHouse.svelte";
</script>

<div>
    <div class="flex mx-2">
        <h1>...</h1>
    </div>
    <div class="flex mx-2">
        <Input
            inputName="zipCode[]"
            label="ZipCode"
            parentWidth="w-full"
            error={errors?.ZipCode}
            required={true}
            on:input={(e) => {
                if(e && e.currentTarget){
                    e.currentTarget.value = e.currentTarget.value.replace(/[^0-9]/g, '')
                }
            }}
        >
            <MdLocationOn slot="icon" />
        </Input>
    </div>
    <div class="flex mx-2">
        <Input
            inputName="city[]"
            label="City"
            parentWidth="w-full"
            error={errors?.City}
        >
            <MdLocationCity slot="icon" />
        </Input>
    </div>
    <div class="flex mx-2">
        <Input
            inputName="street[]"
            label="Street"
            parentWidth="w-full"
            error={errors?.Street}
        >
            <MdLocationOn slot="icon" />
        </Input>
    </div>
    <div class="flex mx-2">
        <Input
            inputName="houseNumber[]"
            label="House Number"
            parentWidth="w-full"
            error={errors?.HouseNumber}
        >
            <GiMushroomHouse slot="icon" />
        </Input>
    </div>
</div>
