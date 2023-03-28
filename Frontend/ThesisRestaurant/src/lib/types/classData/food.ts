import type { FoodPicture } from "./foodPicture";
import type { FoodPrice } from "./foodPrice";
import type { FoodType } from "./foodType";
import type { Ingredient } from "./ingredient";

export interface Food {
    [key: string]: string | number | FoodType | Array<Ingredient> | Array<FoodPicture> | Array<FoodPrice>;
    id: number;
    name: string;
    basePrice: number;
    discountPrice: number;
    foodType: FoodType;
    ingredients: Array<Ingredient>;
    foodPictures: Array<FoodPicture>;
    foodPrices: Array<FoodPrice>;
}

