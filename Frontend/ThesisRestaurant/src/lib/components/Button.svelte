<script lang="ts">
    export let btnClass: string = "btn-success";
    export let disabled: boolean = false;
</script>

<button
    on:click
    {disabled}
    class="{btnClass}"
    ><slot>OK</slot>
</button>

<style>
    button {
        transition: background-color 0.4s ease;
    }
</style>
