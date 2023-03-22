import { API_ROUTE } from "$env/static/private";
import { customFetch } from "$lib/helpers/customFetch";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ parent, cookies, locals, depends }) => {
    depends('app:stock')
    const ingredients = await customFetch(`${API_ROUTE}ingredient`, cookies, { method: "GET" })
        .then(async (res) => await res.json())

    const ingredientTypes = await customFetch(`${API_ROUTE}ingredienttype`, cookies, { method: "GET" })
        .then(async (res) => await res.json())

    const foodTypes = await customFetch(`${API_ROUTE}foodtype`, cookies, { method: "GET" })
        .then(async (res) => await res.json())

    const foodSizes = await customFetch(`${API_ROUTE}foodsize`, cookies, { method: "GET" })
        .then(async (res) => await res.json())

    return {
        user: locals.user,
        ingredients,
        foodTypes,
        ingredientTypes,
        foodSizes
        //nested key-be streameli, ha nincs awaitelve a fetch
        // streamed: {
        //     ingredients
        // }
    }
}