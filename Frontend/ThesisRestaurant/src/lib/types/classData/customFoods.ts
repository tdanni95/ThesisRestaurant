import type { FoodType } from "./foodType";
import type { Ingredient } from "./ingredient";

export interface CustomFood{
    [key: string]: string | number | FoodType | Array<Ingredient>;
    id: number;
    name: string;
    price: number;
    discountPrice: number;
    foodType: FoodType;
    ingredients: Array<Ingredient>;
}

