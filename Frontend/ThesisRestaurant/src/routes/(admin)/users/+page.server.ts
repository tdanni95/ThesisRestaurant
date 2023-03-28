import { API_ROUTE } from "$env/static/private";
import { ChangeFoodSources } from "$lib/helpers/changeFoodSources.server";
import { customFetch } from "$lib/helpers/customFetch";
import type { UserData } from "$lib/types/userData";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ parent, cookies, locals, depends }) => {
    depends('app:users')
    // food/GetAllFoods
    
    const users = await customFetch(`${API_ROUTE}user`, cookies, { method: "GET" })
        .then(async (res) => await res.json())

    return {
        user: locals.user,
        users: users as Array<UserData>
    }
}