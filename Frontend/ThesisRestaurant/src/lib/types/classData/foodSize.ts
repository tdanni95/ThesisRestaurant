import type { FoodType } from "./foodType";

export interface FoodSize {
    [key: string]: string | number | FoodType;
    id: number;
    name: string;
    multiplier: number;
    foodType: FoodType;
}