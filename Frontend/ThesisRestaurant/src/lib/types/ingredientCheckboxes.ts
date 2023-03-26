import type { Ingredient } from "./classData";

export interface IngredientCheckboxes{
    [key: string] : string | Array<Ingredient>
    name: string;
    ingredients: Array<Ingredient>
}