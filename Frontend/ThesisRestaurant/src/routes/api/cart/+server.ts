import { API_ROUTE } from "$env/static/private";
import { customFetch } from "$lib/helpers/customFetch";
import { UserDataFromJwt } from "$lib/helpers/userDataFromJwt";
import { error, json, redirect } from "@sveltejs/kit"
import { url } from "inspector";
import type { RequestHandler } from "./$types"
import type { CartItem } from "$lib/types/cart";

export const POST: RequestHandler = async ({ request, cookies }) => {
    const token = cookies.get('token')
    const ud = UserDataFromJwt(token!)
    const fetchUrl = `${API_ROUTE}order/${ud.id}/cart`
    const data = await request.json()

    const builtData = {
        customFoodIds: data.customFoods,
        orderItems: []
    }

    for (const food of data.foods as Array<CartItem>) {
        //@ts-ignore
        builtData.orderItems.push({ foodId: food.id, foodSizeId: food.foodSizeId, additionalIngredients: food.additionalIngredients })
    }

    
    const response = await customFetch(fetchUrl, cookies, {
        method: "POST", body: JSON.stringify(builtData), headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
        },
    })
    const res = await response.json()
    return json({
        cart: res
    })
}
