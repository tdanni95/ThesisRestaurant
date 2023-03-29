<script lang="ts">
    import { page } from '$app/stores';    
</script>
<div class="mt-20 flex justify-center items-center">
    <h1 class="text-5xl text-red-500">{$page.status}: {$page?.error?.message}</h1>
</div>