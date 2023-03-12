<script lang="ts">
    import { applyAction, enhance } from "$app/forms";
    import AddressForm from "../../lib/components/AddressForm.svelte";

    import FaRegUser from "svelte-icons/fa/FaRegUser.svelte";
    import MdMailOutline from "svelte-icons/md/MdMailOutline.svelte";
    import MdPhoneIphone from "svelte-icons/md/MdPhoneIphone.svelte";
    import MdLockOutline from "svelte-icons/md/MdLockOutline.svelte";

    import { fade, scale } from "svelte/transition";

    import Input from "$lib/components/Input.svelte";
    import type {
        AddressError,
        RegisterErrors,
    } from "$lib/types/registerErrors";
    import TabHeadItem from "$lib/components/TabHeadItem.svelte";

    let addressFormCount: number = 1;

    $: isRemoveBtnDisabled = addressFormCount === 1;

    let list = [1];

    let currentlyShowed = 1;

    const addAddressForm = () => {
        list = [...list, ++addressFormCount];
    };
    const removeAddressForm = () => {
        if (list.length > 1) {
            list = list.filter((n) => n !== addressFormCount);
            addressFormCount--;
            if (currentlyShowed !== 1) {
                currentlyShowed = currentlyShowed - 1;
            }
        }
    };

    let isLoading = false;

    let errors: RegisterErrors = {
        Password: "",
        Email: "",
        FirstName: "",
        LastName: "",
        PhoneNumber: "",
    };

    let AddressErrors: Array<AddressError> = [];
</script>

<form
    use:enhance={({ form }) => {
        isLoading = true;

        return ({ result }) => {
            AddressErrors = [];
            errors = {
                Password: "",
                Email: "",
                FirstName: "",
                LastName: "",
                PhoneNumber: "",
            };
            isLoading = false;

            if (result.errors) {
                for (const err in result.errors) {
                    if (err.includes("[")) {
                        // Addresses[0]
                        let newErrKey = err.split("[")[1];
                        let indexAndKey = newErrKey.split("]");
                        let index = +indexAndKey[0];
                        let addressErrorKey = indexAndKey[1].replace(".", "");
                        if (!AddressErrors[index]) {
                            AddressErrors[index] = {
                                ZipCode: "",
                                City: "",
                                Street: "",
                                HouseNumber: "",
                            };
                        }
                        AddressErrors[index][addressErrorKey] =
                            result.errors[err];
                    } else {
                        errors[err] = result.errors[err];
                    }
                }
            }else if(result.status == 409){
                errors.Email = result.title
            }
            else if (result.status == 200) {
                applyAction(result);
            }
        };
    }}
    method="POST"
    action="api/register"
>
    <div
        class="min-w-screen min-h-screen bg-gray-900 flex items-center justify-center px-5 py-5"
    >
        <div class="bg-gray-100 text-gray-500 rounded-3xl shadow-xl w-full">
            <div class="md:flex w-full">
                <div class="w-full md:w-6/12 py-10 px-5 md:px-10">
                    <div class="text-center  h-4/12">
                        <h1 class="font-bold text-3xl text-gray-900">
                            REGISTER
                        </h1>
                        <p>Enter your information to register</p>
                    </div>
                    <div class="h-5/6">
                        <div class="flex -mx-3">
                            <Input
                                inputName="firstName"
                                label="First name"
                                error={errors.FirstName}
                            >
                                <FaRegUser slot="icon" />
                            </Input>
                            <Input
                                inputName="lastName"
                                label="Last name"
                                error={errors.LastName}
                                ><FaRegUser slot="icon" /></Input
                            >
                        </div>
                        <div class="flex -mx-3">
                            <Input
                                inputName="email"
                                label="Email"
                                parentWidth="w-full"
                                type="email"
                                error={errors.Email}
                                ><MdMailOutline slot="icon" /></Input
                            >
                        </div>
                        <div class="flex -mx-3">
                            <Input
                                inputName="phoneNumber"
                                label="Phone number"
                                parentWidth="w-full"
                                error={errors.PhoneNumber}
                                type="text"><MdPhoneIphone slot="icon" /></Input
                            >
                        </div>
                        <div class="flex -mx-3">
                            <Input
                                inputName="password"
                                label="Password"
                                parentWidth="w-full"
                                type="password"
                                error={errors.Password}
                                ><MdLockOutline slot="icon" /></Input
                            >
                        </div>
                        <div class="flex -mx-3">
                            <Input
                                inputName="passwordAgain"
                                label="Repeat Password"
                                parentWidth="w-full"
                                type="password"
                                error={errors.Password}
                                ><MdLockOutline slot="icon" /></Input
                            >
                        </div>
                    </div>
                    <div class="flex -mx-3 h-1/6">
                        <div class="w-full px-3 mb-5">
                            <button
                                disabled={isLoading}
                                class="block w-full max-w-xs mx-auto bg-indigo-500 hover:bg-indigo-700 focus:bg-indigo-700 text-white rounded-lg px-3 py-3 font-semibold"
                                >REGISTER NOW</button
                            >
                        </div>
                    </div>
                </div>
                <div class="w-full md:w-6/12 py-10 px-5 md:px-10">
                    <div class="text-center  h-4/12">
                        <h1 class="font-bold text-3xl text-gray-900">
                            ADDRESSES
                        </h1>
                        <p>
                            Enter address details, currently: {addressFormCount}
                        </p>
                    </div>
                    <div class="h-5/6 overflow-auto w-full">
                        <div class='mb-4 text-sm font-medium text-center text-gray-500 border-b border-gray-200 dark:text-gray-400 dark:border-gray-700'>
                            <ul class="flex flex-wrap -mb-px">
                                {#each list as n (n)}
                                <TabHeadItem
                                    id={n}
                                    activeTabValue={currentlyShowed}
                                    on:click={() => {
                                        currentlyShowed = n;
                                    }}
                                    tabStyle="underline"
                                    >Address {n}</TabHeadItem>
                                    {/each}
                                </ul>
                            </div>
                        {#each list as n (n)}
                            <div 
                                style="display: {currentlyShowed === n
                                    ? ''
                                    : 'none'}"
                            >
                                <AddressForm
                                    idx={n}
                                    errors={AddressErrors[n - 1]}
                                />
                            </div>
                        {/each}
                    </div>

                    <div class="flex -mx-3 h-1/6">
                        <div class="w-full px-3 mb-5 max">
                            <button
                                on:click|preventDefault={addAddressForm}
                                class="block w-full max-w-xs mx-auto bg-green-500 hover:bg-green-700 text-white rounded-lg px-3 py-3 font-semibold"
                                >Add new address</button
                            >
                        </div>
                        <div class="w-full px-3 mb-5">
                            <button
                                on:click|preventDefault={removeAddressForm}
                                disabled={isRemoveBtnDisabled}
                                class="block w-full max-w-xs mx-auto bg-red-500 hover:bg-red-700 text-white rounded-lg px-3 py-3 font-semibold disabled:cursor-not-allowed disabled:bg-red-400"
                                >Remove last address</button
                            >
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</form>

<style>
    button {
        transition: background-color 0.4s ease;
    }
</style>
