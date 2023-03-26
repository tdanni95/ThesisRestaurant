<script lang="ts">
    import Swal from "sweetalert2";
    import { invalidate } from "$app/navigation";
    import { page } from "$app/stores";
    import AddressForm from "$lib/components/AddressForm.svelte";
    import Button from "$lib/components/Button.svelte";
    import TabHeadItem from "$lib/components/TabHeadItem.svelte";
    import toastStore from "$lib/stores/toastStore";
    import type { Address } from "$lib/types/address";
    import type { UserData } from "$lib/types/userData";
    import type { ApiErrorResponse } from "$lib/types/apiErrorResponse";
    import type {
        AddressError,
        RegisterErrors,
    } from "$lib/types/registerErrors";

    import MdDelete from "svelte-icons/md/MdDelete.svelte";
    import Input from "$lib/components/Input.svelte";
    import FaRegUser from "svelte-icons/fa/FaRegUser.svelte";
    import MdMailOutline from "svelte-icons/md/MdMailOutline.svelte";
    import MdPhoneIphone from "svelte-icons/md/MdPhoneIphone.svelte";
    import FaRegEdit from "svelte-icons/fa/FaRegEdit.svelte";
    import MdLockOutline from "svelte-icons/md/MdLockOutline.svelte";
    import Modal from "$lib/components/Modal.svelte";
    import MdAdd from "svelte-icons/md/MdAdd.svelte";

    let userData = $page.data.userData as UserData;
    let currentlyShowed = userData.addresses[0].id;

    $: errorText = "";

    let errors: RegisterErrors = {
        Password: "",
        Email: "",
        FirstName: "",
        LastName: "",
        PhoneNumber: "",
    };

    let AddressErrors: AddressError = {} as AddressError;
    let apiResponsErrors: Array<ApiErrorResponse> = [];

    let isLoading = false;

    const invalidateLoad = () => {
        invalidate("app:profile");
    };

    const updateAddressData = async (id: number) => {
        isLoading = true;
        AddressErrors = {} as AddressError;
        //TODO find selected address
        const response = await fetch("api/address", {
            method: "PUT",
            body: JSON.stringify(userData.addresses[0]),
        });
        const res = await response.json();
        isLoading = false;

        if (res.errors) {
            errorText = res.title;
            apiResponsErrors = res.errors;
            toastStore.error(errorText, 2000);
            handleAddressErrors(AddressErrors);
            invalidateLoad();
        } else {
            errorText = "";
            toastStore.success("Address modified successully!", 2000);
        }
    };

    const updateUserData = async () => {
        isLoading = true;
        errors = {} as RegisterErrors;

        const response = await fetch("api/user/?action=modify", {
            method: "PUT",
            body: JSON.stringify(userData),
        });

        const res = await response.json();
        isLoading = false;
        if (res.status === 409) {
            errorText = res.title;
            errors.Email = errorText;
            toastStore.error(errorText, 2000);
        } else if (res.errors) {
            errorText = res.title;
            apiResponsErrors = res.errors;
            toastStore.error(errorText, 2000);
            handleUserErrors();
            invalidateLoad();
        } else {
            errorText = "";
            toastStore.success("Data modified successully!", 2000);
        }
    };

    const passwordData = {
        OldPassword: "",
        NewPassword: "",
        NewPasswordAgain: "",
    };

    const changeUserPassword = async () => {
        isLoading = true;
        const response = await fetch("api/user/?action=password", {
            method: "PUT",
            body: JSON.stringify(passwordData),
        });
        const res = await response.json();
        if (res.msg) {
            errorText = "";
            passwordData.OldPassword = "";
            passwordData.NewPassword = "";
            passwordData.NewPasswordAgain = "";
            toastStore.success("Password modified successully!", 2000);
        } else {
            errorText = res.title;
            apiResponsErrors = res.errors;

            for (const err in res.errors) {
                if (err === "Auth.InvalidCredentials") {
                    // @ts-ignore
                    errorText += ` ${res.errors[err]}`;
                } else {
                    errors.Password = res.errors[err];
                }
            }

            toastStore.error(errorText, 2000);
        }
        isLoading = false;
    };

    const handleAddressErrors = (pushTo: AddressError) => {
        for (const err in apiResponsErrors) {
            pushTo[err] = apiResponsErrors[err][0] as string;
        }
    };

    const handleUserErrors = () => {
        for (const err in apiResponsErrors) {
            errors[err] = apiResponsErrors[err][0] as string;
        }
    };
    
    let modalVisible = false;

    const closeModal = () => {
        modalVisible = false;
    };

    let modalAddressErrors = {} as AddressError;
    let modalAddressData = {} as Address;

    const addNewAddress = async () => {
        isLoading = true;
        const response = await fetch(`api/address`, {
            body: JSON.stringify(modalAddressData),
            method: "POST",
        });
        const res = await response.json();
        isLoading = false;

        modalAddressErrors = {} as AddressError;

        if (res.errors) {
            errorText = res.title;
            apiResponsErrors = res.errors;
            toastStore.error(errorText, 2000);
            handleAddressErrors(modalAddressErrors);
        } else {
            modalAddressData.houseNumber = "";
            modalAddressData.street = "";
            modalAddressData.cit = "";
            errorText = "";
            toastStore.success("Address created successully!", 2000);
            invalidateLoad();
            modalVisible = false;
        }
    };

    const deleteAddress = async (id: number) => {
        await Swal.fire({
            title: "Are you sure you want to delete the address?",
            icon: "question",
            showCancelButton: true,
            showLoaderOnConfirm: true,
            preConfirm: async () => {
                isLoading = true;
                const response = await fetch(`api/address`, {
                    body: JSON.stringify({ id: id }),
                    method: "DELETE",
                });
                isLoading = false;
                const res = await response.json();

                isLoading = false;
                if (res.msg && res.msg == "success") {
                    toastStore.success("Address deleted successfully", 2000);
                    invalidateLoad();
                } else {
                    errorText = res.title;
                    toastStore.error(errorText, 2000);
                }
            },
        });
    };
</script>

{#if modalVisible}
    <Modal
        title="Add new address"
        close={closeModal}
        bind:visible={modalVisible}
    >
        <div slot="body" class="w-full">
            <AddressForm
                bind:addressData={modalAddressData}
                errors={modalAddressErrors}
            />
        </div>
        <div slot="footer" class="flex">
            <Button
                disabled={isLoading}
                btnClass="btn-success"
                on:click={addNewAddress}>Save address</Button
            >
            <Button
                disabled={isLoading}
                btnClass="btn-primary"
                on:click={closeModal}>Close</Button
            >
        </div>
    </Modal>
{/if}
<div
    class="min-w-screen min-h-screen  flex items-center justify-center px-5 py-5"
>
    <div class="bg-gray-200 text-gray-500 rounded-3xl shadow-xl w-10/12">
        <div class="text-center mt-5  h-4/12">
            <h1 class="font-bold text-4xl text-red-600">
                {errorText}
            </h1>
        </div>
        <div class="md:flex w-full">
            <div class="w-full md:w-6/12 py-10 px-5 md:px-10">
                <div class="text-center  h-4/12">
                    <h1 class="font-bold text-3xl text-gray-900">User data</h1>
                </div>
                <div class="flex -mx-3">
                    <Input
                        inputName="firstName"
                        label="First name"
                        error={errors.FirstName}
                        bind:value={userData.firstName}
                    >
                        <FaRegUser slot="icon" />
                    </Input>
                    <Input
                        inputName="lastName"
                        label="Last name"
                        error={errors.LastName}
                        bind:value={userData.lastName}
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
                        bind:value={userData.email}
                        ><MdMailOutline slot="icon" /></Input
                    >
                </div>
                <div class="flex -mx-3">
                    <Input
                        inputName="phoneNumber"
                        label="Phone number"
                        parentWidth="w-full"
                        error={errors.PhoneNumber}
                        bind:value={userData.phoneNumber}
                        type="text"><MdPhoneIphone slot="icon" /></Input
                    >
                </div>

                <Button
                    disabled={isLoading}
                    btnClass="btn-success"
                    on:click={() => {
                        updateUserData();
                    }}
                >
                    <FaRegEdit slot="icon" />
                    Modify user data
                </Button>
                <div class="text-center  h-4/12 mt-5">
                    <h1 class="font-bold text-3xl text-gray-900">
                        Change password
                    </h1>
                </div>
                <div class="flex -mx-3">
                    <Input
                        bind:value={passwordData.OldPassword}
                        inputName="currentPassword"
                        label="Current Password"
                        parentWidth="w-full"
                        type="password"
                        error={errors.Password}
                        ><MdLockOutline slot="icon" /></Input
                    >
                </div>
                <div class="flex -mx-3">
                    <Input
                        bind:value={passwordData.NewPassword}
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
                        bind:value={passwordData.NewPasswordAgain}
                        inputName="passwordAgain"
                        label="Repeat Password"
                        parentWidth="w-full"
                        type="password"
                        error={errors.Password}
                        ><MdLockOutline slot="icon" /></Input
                    >
                </div>
                <Button
                    disabled={isLoading}
                    btnClass="btn-success"
                    on:click={() => {
                        changeUserPassword();
                    }}
                >
                    <FaRegEdit slot="icon" />
                    Change password
                </Button>
            </div>
            <div class="w-full md:w-6/12 py-10 px-5 md:px-10">
                <div class="text-center  h-4/12">
                    <h1 class="font-bold text-3xl text-gray-900">Addresses</h1>
                </div>
                <ul class="flex flex-wrap -mb-px">
                    {#each userData.addresses as address (address.id)}
                        <TabHeadItem
                            id={address.id}
                            activeTabValue={currentlyShowed}
                            on:click={() => {
                                currentlyShowed = address.id;
                            }}
                            tabStyle="underline"
                            >Address {address.id}</TabHeadItem
                        >
                    {/each}
                </ul>
                {#each userData.addresses as address (address.id)}
                    <div
                        style="display: {currentlyShowed == address.id
                            ? ''
                            : 'none'};"
                    >
                        <AddressForm
                            bind:addressData={address}
                            errors={AddressErrors}
                        >
                            <div class="w-full px-3 mb-5">
                                <Button
                                    btnClass="btn-error"
                                    on:click={() => {
                                        deleteAddress(address.id);
                                    }}
                                >
                                    <MdDelete slot="icon" />
                                    Delete
                                </Button>
                            </div>
                            <div class="w-full px-3 mb-5">
                                <Button
                                    btnClass="btn-success"
                                    on:click={() => (modalVisible = true)}
                                >
                                    <MdAdd slot="icon" />
                                    Add new address
                                </Button>
                            </div>
                            <div class="w-full px-3 mb-5">
                                <Button
                                    disabled={isLoading}
                                    btnClass="btn-success"
                                    on:click={() => {
                                        updateAddressData(address.id);
                                    }}
                                >
                                    <FaRegEdit slot="icon" />
                                    Modify
                                </Button>
                            </div>
                        </AddressForm>
                    </div>
                {/each}
            </div>
        </div>
    </div>
</div>
