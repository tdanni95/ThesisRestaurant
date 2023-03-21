import { API_ROUTE } from "$env/static/private";
import { customFetch } from "$lib/helpers/customFetch";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ parent, cookies, locals, depends }) => {
    depends('app:stock')
    const ingredients = await customFetch(`${API_ROUTE}ingredient`, cookies, { method: "GET" })
        .then(async (res) => await res.json())

    return {
        user: locals.user,
        ingredients,
        //nested key-be streameli, ha nincs awaitelve a fetch
        // streamed: {
        //     ingredients
        // }
    }
}