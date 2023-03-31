<script lang="ts">
    export let btnClass: string = "btn-success";
    export let disabled: boolean = false;
</script>

<button on:click {disabled} class="{btnClass}" class:has-icon={$$slots.icon}>
    {#if $$slots.icon}
        <div class="leftContent h-8">
            <slot name="icon" />
        </div>
    {/if}
    <slot />
</button>

<style>
    button {
        transition: all 0.4s ease;
    }
    .has-icon{
        display: flex;
        justify-content: center;
        align-items: center;
        text-align: center;
    }
    .leftContent{
        display: inline-block;
    }
</style>
