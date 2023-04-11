import { API_ROUTE } from "$env/static/private";
import { customFetch } from "$lib/helpers/customFetch";
import type { Order } from "$lib/types/classData";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ parent, cookies, locals}) => {
    const orderDataResponse = await customFetch(`${API_ROUTE}order/${locals.user?.id}`, cookies)
    const orderData = await orderDataResponse.json()
    
    return {
        orderData: orderData as Order,
        user: locals.user
    }
}


