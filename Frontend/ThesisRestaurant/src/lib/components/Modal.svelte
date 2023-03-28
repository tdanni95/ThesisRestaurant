<script lang="ts">
    import { fade } from 'svelte/transition';
    export let title = "Modal";

    export let close: () => void;

    export let visible = true;
    export let width = "w-3/4"
    
</script>

<div class="fixed inset-0 z-50 flex items-center justify-center modalHolder">
    {#if visible}
        <div
            class="bg-white rounded-lg shadow-lg p-8 m-4 {width} max-h-full overflow-y-auto z-50"
            transition:fade={{ duration: 300 }}
        >
            <slot name="header">
                <h2 class="text-gray-900 font-bold text-xl mb-4">{title}</h2>
            </slot>
            <slot name="body">
                <p class="text-gray-700 text-sm mb-4">
                    Modal content goes here
                </p>
            </slot>
            <slot name="footer">
                <button
                    class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded mr-2"
                    on:click={() => close()}
                >
                    Close
                </button>
            </slot>
        </div>
        <!-- svelte-ignore a11y-click-events-have-key-events -->
        <div
            class="bg-black opacity-25 w-screen h-screen absolute z-40"
            on:click={() => (visible = false)}
        />
    {/if}
</div>

