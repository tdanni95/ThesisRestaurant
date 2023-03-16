import { API_ROUTE } from "$env/static/private";
import { customFetch } from "$lib/helpers/customFetch";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ fetch, parent, cookies, locals }) => {
    const res = await parent()
    const accessToken = cookies.get('token')
    
    const userDataResponse = await customFetch(`${API_ROUTE}user/13`, cookies)

    const userData = await userDataResponse.json()

    return {
        userData,
        user: locals.user
    }
}