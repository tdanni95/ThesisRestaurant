<script lang="ts">
    import NavigationBar from "$lib/components/NavigationBar.svelte";
    import "../app.css";
    import type { LayoutData } from "./$types";
</script>

<svelte:head>
    <title>ThesisRestaurant</title>
</svelte:head>
<NavigationBar />
<slot />
