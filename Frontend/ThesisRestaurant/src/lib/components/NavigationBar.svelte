<script lang="ts">
    import FaRegUser from "svelte-icons/fa/FaRegUser.svelte";
    import FaPowerOff from "svelte-icons/fa/FaPowerOff.svelte";

    import { page } from "$app/stores";
    import { enhance } from "$app/forms";
</script>

<nav class="bg-gray-700">
    <div class="container mx-auto py-4 flex justify-between items-center">
        <h1 class="text-2xl font-bold text-gray-50">
            <a href="/">ThesisRestaurant</a>
        </h1>
        <div class="flex space-x-10">
            <div class="flex items-center space-x-2">
                <span>
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        class="h-8 w-8 text-yellow-400"
                        fill="none"
                        viewBox="0 0 24 24"
                        stroke="currentColor"
                    >
                        <path
                            stroke-linecap="round"
                            stroke-linejoin="round"
                            stroke-width="2"
                            d="M12 6.253v13m0-13C10.832 5.477 9.246 5 7.5 5S4.168 5.477 3 6.253v13C4.168 18.477 5.754 18 7.5 18s3.332.477 4.5 1.253m0-13C13.168 5.477 14.754 5 16.5 5c1.747 0 3.332.477 4.5 1.253v13C19.832 18.477 18.247 18 16.5 18c-1.746 0-3.332.477-4.5 1.253"
                        />
                    </svg>
                </span>
                <span class="text-gray-50">Topics</span>
            </div>
            <div class="flex items-center space-x-2">
                <span>
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        class="h-8 w-8 text-yellow-400"
                        fill="none"
                        viewBox="0 0 24 24"
                        stroke="currentColor"
                    >
                        <path
                            stroke-linecap="round"
                            stroke-linejoin="round"
                            stroke-width="2"
                            d="M19 20H5a2 2 0 01-2-2V6a2 2 0 012-2h10a2 2 0 012 2v1m2 13a2 2 0 01-2-2V7m2 13a2 2 0 002-2V9a2 2 0 00-2-2h-2m-4-3H9M7 16h6M7 8h6v4H7V8z"
                        />
                    </svg>
                </span>
                <span class="text-gray-50">Blog</span>
            </div>
            <div class="flex items-center space-x-2">
                <span>
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        class="h-8 w-8 text-yellow-400"
                        fill="none"
                        viewBox="0 0 24 24"
                        stroke="currentColor"
                    >
                        <path
                            stroke-linecap="round"
                            stroke-linejoin="round"
                            stroke-width="2"
                            d="M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0M15 7a3 3 0 11-6 0 3 3 0 016 0zm6 3a2 2 0 11-4 0 2 2 0 014 0zM7 10a2 2 0 11-4 0 2 2 0 014 0z"
                        />
                    </svg>
                </span>
                <span class="text-gray-50">Certificate Training</span>
            </div>
        </div>
        <div class="lg:flex hidden items-center space-x-2">
            <span class="h-5 text-yellow-400">
                <FaRegUser />
            </span>
            {#if $page.data.user}
                <a href="/profile" class="text-gray-50"
                    >{$page.data.user.firstName}
                    {$page.data.user.lastName} - {$page.data.user.role}</a
                >
                <span class="h-5 text-yellow-400">
                    <FaPowerOff />
                </span>
                <form method="post" action="/login?/logout" use:enhance>
                    <button class="text-gray-50">Logout</button>
                </form>
            {:else}
                <a href="/login" class="text-gray-50">Login</a>
            {/if}
        </div>
    </div>
</nav>
