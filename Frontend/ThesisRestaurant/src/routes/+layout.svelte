<script lang="ts">
    import NavigationBar2 from "$lib/components/NavigationBar2.svelte";
    import Toast from "$lib/components/Toast.svelte";
    import "../app.css";

</script>

<svelte:head>
    <title>ThesisRestaurant</title>
</svelte:head>
<NavigationBar2 />


<Toast />
<slot />
