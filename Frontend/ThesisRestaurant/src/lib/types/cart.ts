export interface Cart{
    customFoods: Array<CartCustomItem>;
    foods : Array<CartItem>;
}

export type CartCustomItem = {
    guid: number;
    id: number;
}

export type CartItem = {
    guid: number;
    id: number;
    foodSizeId: number;
    additionalIngredients : Array<number>;
}