export interface Cart{
    customFoods?: Array<number>;
    foods? : Array<CartItem>;
}

export type CartItem = {
    id: number;
    foodSizeId: number;
    additionalIngredients? : Array<number>;
}