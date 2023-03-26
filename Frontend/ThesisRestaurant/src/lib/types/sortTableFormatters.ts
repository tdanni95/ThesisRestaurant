import type { Food, FoodSize, FoodType, Ingredient, IngredientType } from "./classData";

export interface SortTableFormatters <T>{
    name: string;
    callBack: (obj:T) => string
}