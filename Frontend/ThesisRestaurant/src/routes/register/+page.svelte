<script lang="ts">
    import { applyAction, enhance } from "$app/forms";
    import AddressForm from "../../lib/components/AddressForm.svelte";

    import FaRegUser from "svelte-icons/fa/FaRegUser.svelte";
    import MdMailOutline from "svelte-icons/md/MdMailOutline.svelte";
    import MdPhoneIphone from "svelte-icons/md/MdPhoneIphone.svelte";
    import MdLockOutline from "svelte-icons/md/MdLockOutline.svelte";

    import Input from "$lib/components/Input.svelte";
    import type {
        AddressError,
        RegisterErrors,
    } from "$lib/types/registerErrors";
    import TabHeadItem from "$lib/components/TabHeadItem.svelte";
    import Button from "$lib/components/Button.svelte";
    import type { ApiErrorResponse } from "$lib/types/apiErrorResponse";
    import FormAd from "$lib/components/FormAd.svelte";
    import { invalidateAll } from "$app/navigation";

    let addressFormCount: number = 1;
    let errorText = "";

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
    let apiResponsErrors: Array<ApiErrorResponse> = [];

    const handleApiResponseErrors = () => {
        for (const err in apiResponsErrors) {
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

                AddressErrors[index][addressErrorKey] = apiResponsErrors[
                    err
                ][0] as string;
            } else {
                errors[err] = apiResponsErrors[err][0] as string;
            }
        }
    };
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
            // @ts-ignore
            errorText = result.title ? result.title : "";
            // @ts-ignore
            apiResponsErrors = result.errors;
            handleApiResponseErrors();
            console.log(result);
            if (result.status == 409) {
                // @ts-ignore
                errors.Email = result.title;
                // @ts-ignore
            } else if (result.id) {
                applyAction(result);
                invalidateAll();
            }
            console.log(result.status);
        };
    }}
    method="POST"
    action="api/register"
>
    <div
        class="min-w-screen min-h-screen  flex items-center justify-center px-5 py-5"
    >
        <div class="bg-gray-200 text-gray-500 rounded-3xl shadow-xl w-10/12">
            <div class="md:flex w-full">
                <FormAd />
                <div class="w-full md:w-6/12 py-10 px-5 md:px-10">
                    <div class="text-center  h-4/12">
                        <h1 class="font-bold text-3xl text-gray-900">
                            Register
                        </h1>
                        <h1 class="font-bold text-4xl text-red-600">
                            {errorText}
                        </h1>
                        <p>
                            Already registered? <a
                                class="underline "
                                href="/login">Login</a
                            >
                        </p>
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
                        <div class="text-center  h-4/12">
                            <h1 class="font-bold text-3xl text-gray-900">
                                ADDRESSES
                            </h1>
                            <p>
                                Enter address details, currently: {addressFormCount}
                            </p>
                        </div>
                        <ul class="flex flex-wrap -mb-px">
                            {#each list as n (n)}
                                <TabHeadItem
                                    id={n}
                                    activeTabValue={currentlyShowed}
                                    on:click={() => {
                                        currentlyShowed = n;
                                    }}
                                    tabStyle="underline"
                                    >Address {n}</TabHeadItem
                                >
                            {/each}
                        </ul>
                        {#each list as n (n)}
                            <div
                                style="display: {currentlyShowed === n
                                    ? ''
                                    : 'none'}"
                            >
                                <AddressForm errors={AddressErrors[n - 1]} />
                            </div>
                        {/each}
                    </div>
                    <div class="flex mt-10">
                        <div class="w-full px-3 mb-5 max">
                            <Button
                                on:click={(e) => {
                                    e.preventDefault();
                                    addAddressForm();
                                }}
                                btnClass="btn-success">Add new address</Button
                            >
                        </div>
                        <div class="w-full px-3 mb-5">
                            <Button
                                disabled={isRemoveBtnDisabled}
                                on:click={(e) => {
                                    e.preventDefault();
                                    removeAddressForm();
                                }}
                                btnClass="btn-error">Remove last address</Button
                            >
                        </div>
                        <div class="w-full px-3 mb-5">
                            <Button disabled={isLoading} btnClass="btn-primary"
                                >Register</Button
                            >
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</form>
