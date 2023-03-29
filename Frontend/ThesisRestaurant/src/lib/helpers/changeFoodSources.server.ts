import { API_ROUTE } from "$env/static/private";
import type { Food } from "$lib/types/classData";

export function ChangeFoodSources(food: Food) {
    // API_ROUTE
    if (!food.foodPictures.length) {
        food.foodPictures.push({ id: 0, src: "pizza.png" })
        return
    }
    for (const foodPic of food.foodPictures) {
        foodPic.src = `${API_ROUTE}${foodPic.src}`
    }
}