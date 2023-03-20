import { API_ROUTE } from "$env/static/private";
import { customFetch } from "$lib/helpers/customFetch";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ parent, cookies, locals, depends }) => {
    depends('app:profile')
    const userDataResponse = await customFetch(`${API_ROUTE}user/${locals.user?.id}`, cookies)
    const userData = await userDataResponse.json()
    
    return {
        userData,
        user: locals.user
    }
}