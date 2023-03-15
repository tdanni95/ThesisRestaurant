<script lang="ts">

    export let parentWidth: string = "w-1/2";
    export let label: string;
    export let inputName: string;
    export let type: string = "text";
    export let required: boolean = true;
    export let value: string | number = "";

    export let error: string | undefined = undefined;
    export let regex: RegExp | undefined = undefined;

    let actualName = inputName.replace("[]", "")

    $: hasError = Boolean(error);
</script>

<div class="{parentWidth} px-3 mb-2">
    <label for={inputName} class="text-xs font-semibold px-1">{label}</label>
    <div class="flex">
        <div
            class="w-6 z-10 pr-1 text-center pointer-events-none flex items-right justify-left"
        >
            <slot name="icon" />
        </div>
        <input
            {type}
            class="w-full -ml-10 pl-10 pr-3 py-2 rounded-lg border-2 outline-none focus:border-indigo-500 {hasError
                ? 'border-red-500'
                : 'border-gray-200'}"
            name={inputName}
            {required}
            placeholder={label}
            {value}
            on:keyup={(e) => {
                value = e.currentTarget.value;
                hasError = false;
            }}
            on:input={(e) => {
                if (!regex) return;
                e.currentTarget.value = e.currentTarget.value.replace(
                    regex,
                    ""
                );
            }}
            on:input
        />
    </div>
    <div class="h-3">
        {#if error && hasError}
            <span
                class="flex items-center font-medium tracking-wide text-red-500 text-xs"
            >
                {error}
            </span>
        {/if}
    </div>
</div>

<style></style>
