import type { FoodType } from "./foodType";

export interface FoodSize {
    [key: string]: string | number | FoodType;
    Id: number;
    Name: string;
    Multiplier: number;
    FoodType: FoodType;
}