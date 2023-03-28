<script lang="ts">
    import { page } from "$app/stores";
    import SortTable from "$lib/components/SortTable.svelte";
    import type { PageData } from "./$types";
    import type { SortTableFormatters } from "$lib/types/sortTableFormatters";
    import toastStore from "$lib/stores/toastStore";
    import Swal from "sweetalert2";
    import type { UserData } from "$lib/types/userData";
    import Modal from "$lib/components/Modal.svelte";
    import Button from "$lib/components/Button.svelte";
    import Select from "$lib/components/Select.svelte";
    import type { UserRole } from "$lib/types/classData";
    import { invalidate } from "$app/navigation";

    export let data: PageData;

    $: users = data.users;

    const roleFormatter = (user: UserData) => {
        switch (user.level) {
            case 1:
                return "AppUser";
            case 2:
                return "Employee";
            case 3:
                return "Admin";
            default:
                return "Unknown";
        }
    };
    const RoleFormatter: SortTableFormatters<UserData> = {
        name: "role",
        callBack: roleFormatter,
    };

    const addressFormatter = (user: UserData) => {
        let str = "";
        for (const address of user.addresses) {
            str += `${address.zipCode}, ${address.city} ${address.street} ${address.houseNumber}<br>`;
        }
        return str;
    };

    const AddressFormatter: SortTableFormatters<UserData> = {
        name: "addresses",
        callBack: addressFormatter,
    };

    let isLoading = false;
    let currentUser: UserData;
    let modalVisible = false;
    let newLevel:number;

    const userLevelUpdater = async (id: number) => {
        if (!id) return;
        const user = users.find((u) => u.id === id);
        if (!user) {
            toastStore.error("User not found", 2000);
            return;
        }
        currentUser = user;
        newLevel = currentUser.level
        modalVisible = true;
    };

    const closeModal = () => {
        modalVisible = false;
    };

    const roles: Array<UserRole> = [
        { id: 1, name: "AppUser" },
        { id: 2, name: "Employee" },
        { id: 3, name: "Admin" },
    ];

    const handleSelection = (id:number) => {
        if(!id) return
        newLevel = id
    }

    const invalidateLoad = () => {
        invalidate('app:users')
    }

    const saveLevel = async () => {
        const dataToSend = {
            id: currentUser.id,
            level: newLevel
        }
        const response = await fetch(`api/user/?action=level`, {method: "PUT", body: JSON.stringify(dataToSend)})
        const res = await response.json()
        if(res.msg){
            toastStore.success("Level modified successfully!", 2000)
            invalidateLoad()
            modalVisible = false
        }else{
            toastStore.error(res.title, 2000)
        }
    }
</script>

{#if modalVisible}
    <Modal
        width={"w-2/4"}
        title="Modify role of {currentUser.firstName} {currentUser.lastName}"
        close={closeModal}
        bind:visible={modalVisible}
    >
        <div slot="body" class="w-full">
            <Select
                data={roles}
                inputName="role"
                label="Role"
                value={currentUser.level}
                on:selectionchange={(e) => {
                    handleSelection(+e.detail.id)
                }}
            />
        </div>
        <div slot="footer" class="flex">
            <Button disabled={isLoading} btnClass="btn-success" on:click={saveLevel}
                >Save role</Button
            >
            <Button
                disabled={isLoading}
                btnClass="btn-primary"
                on:click={closeModal}>Close</Button
            >
        </div>
    </Modal>
{/if}

<div class="min-w-screen flex items-center justify-center px-5 py-5">
    <div class="bg-gray-200 text-gray-500 rounded-3xl shadow-xl w-11/12 py-10">
        <h1 class="font-bold text-3xl text-center text-gray-900">Foods</h1>
        <div class="md:flex w-full">
            <div class="w-full pb-10 px-5 md:px-10">
                <SortTable
                    columns={[
                        "email",
                        "firstName",
                        "lastName",
                        "phoneNumber",
                        "role",
                        "addresses",
                    ]}
                    formatters={[RoleFormatter, AddressFormatter]}
                    ogArray={users}
                    editFunc={() => {}}
                    deleteFunc={() => {}}
                >
                    <td slot="actionButtons" class="text-center">
                        <!-- svelte-ignore a11y-click-events-have-key-events -->
                        <p
                            class="font-xl text-green-500 hover:text-green-700 transition-colors cursor-pointer"
                            on:click={(e) => {
                                const row = e.currentTarget.closest("tr");
                                if (!row || !row.dataset.id) return;
                                userLevelUpdater(+row.dataset.id);
                            }}
                        >
                            Change role
                        </p>
                    </td>
                </SortTable>
            </div>
        </div>
    </div>
</div>
