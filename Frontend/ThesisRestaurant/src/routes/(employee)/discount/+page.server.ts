import { API_ROUTE } from "$env/static/private";
import { ChangeFoodSources } from "$lib/helpers/changeFoodSources.server";
import { customFetch } from "$lib/helpers/customFetch";
import type { Food } from "$lib/types/classData";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ parent, cookies, locals, depends }) => {
    depends('app:discount')
    // food/GetAllFoods

    const foods = await customFetch(`${API_ROUTE}food`, cookies, { method: "GET" })
        .then(async (res) => await res.json())

    return {
        user: locals.user,
        foods: foods as Array<Food>
    }
}