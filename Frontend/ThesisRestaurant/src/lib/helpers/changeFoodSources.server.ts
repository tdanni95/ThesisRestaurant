import { API_ROUTE } from "$env/static/private";
import type { Food} from "$lib/types/classData";

export function ChangeFoodSources(food:Food){
    // API_ROUTE
    for(const foodPic of food.foodPictures){
        foodPic.src = `${API_ROUTE}${foodPic.src}`
    }
}