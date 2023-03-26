<script lang="ts">
    import NavigationBar from "$lib/components/NavigationBar.svelte";
    import NavigationBar2 from "$lib/components/NavigationBar2.svelte";
    import Toast from "$lib/components/Toast.svelte";
    import "../app.css";
    import type { LayoutData } from "./$types";
</script>

<svelte:head>
    <title>ThesisRestaurant</title>
</svelte:head>
<NavigationBar2 />
<Toast />
<slot />
