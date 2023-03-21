import type { IngredientType } from "./ingredientType";

export interface Ingredient{
    [key: string]: string | number | IngredientType;
    id: number;
    name: string;
    price: number;
    ingredientType: IngredientType;
}