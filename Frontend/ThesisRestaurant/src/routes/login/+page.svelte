<script lang="ts">
    import FormAd from "$lib/components/FormAd.svelte";
    import { applyAction, enhance } from "$app/forms";

    import FaRegUser from "svelte-icons/fa/FaRegUser.svelte";
    import MdMailOutline from "svelte-icons/md/MdMailOutline.svelte";
    import MdPhoneIphone from "svelte-icons/md/MdPhoneIphone.svelte";
    import MdLockOutline from "svelte-icons/md/MdLockOutline.svelte";
    import Input from "$lib/components/Input.svelte";
    import type { ApiErrorResponse } from "$lib/types/apiErrorResponse";
    import Button from "$lib/components/Button.svelte";
    import { redirect } from "@sveltejs/kit";
    import { invalidateAll } from "$app/navigation";

    let errorText: string = "";
    let isLoading = false;

</script>

<form
    use:enhance={({ form }) => {
        isLoading = true;

        return ({ result }) => {
            errorText = "";
            isLoading = false;
            if (result.id) {
                applyAction(result);
                invalidateAll()
                // redirect(302, '/')
            } else {
                errorText = result.title;
                for (const err in result.errors) {
                    if (err === "Auth.InvalidCredentials") {
                        errorText += ` ${result.errors[err]}`;
                    }
                }
            }
        };
    }}
    method="POST"
    action="api/login"
>
    <div
        class="min-w-screen min-h-screen  flex items-center justify-center px-5 py-5"
    >
        <div class="bg-gray-200 text-gray-500 rounded-3xl shadow-xl w-10/12">
            <div class="md:flex w-full">
                <FormAd />
                <div class="w-full md:w-6/12 py-10 px-5 md:px-10">
                    <div class="text-center  h-4/12">
                        <h1 class="font-bold text-3xl text-gray-900">Login</h1>
                        <h1 class="font-bold text-3xl text-red-600">
                            {errorText}
                        </h1>
                        <p>
                            Not yet registered? <a
                                class="underline "
                                href="/register">Register</a
                            >
                        </p>
                        <p>Enter email and password to login</p>
                    </div>
                    <div class="flex -mx-3">
                        <Input
                            inputName="email"
                            label="Email"
                            parentWidth="w-full"
                            type="email"><MdMailOutline slot="icon" /></Input
                        >
                    </div>
                    <div class="flex -mx-3">
                        <Input
                            inputName="password"
                            label="Password"
                            parentWidth="w-full"
                            type="password"><MdLockOutline slot="icon" /></Input
                        >
                    </div>
                    <div class="flex mt-10 justify-center">
                        <div class="w-full px-3 mb-5 text-center">
                            <Button disabled={isLoading} btnClass="btn-success"
                                >Login</Button
                            >
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</form>
