import { API_ROUTE } from "$env/static/private";
import { ChangeFoodSources } from "$lib/helpers/changeFoodSources.server";
import { customFetch } from "$lib/helpers/customFetch";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async ({ parent, cookies, locals, depends }) => {
    depends('app:productManagement')
    // food/GetAllFoods

    const foods = await customFetch(`${API_ROUTE}food`, cookies, { method: "GET" })
        .then(async (res) => await res.json())

    const ingredients = await customFetch(`${API_ROUTE}ingredient`, cookies, { method: "GET" })
        .then(async (res) => await res.json())

        const ingredientTypes = await customFetch(`${API_ROUTE}ingredienttype`, cookies, { method: "GET" })
        .then(async (res) => await res.json())

    const foodTypes = await customFetch(`${API_ROUTE}foodtype`, cookies, { method: "GET" })
        .then(async (res) => await res.json())


    if(foods){
        for(const food of foods){
            ChangeFoodSources(food)
        }
    }

    return {
        user: locals.user,
        foods,
        ingredients,
        ingredientTypes,
        foodTypes
    }
}