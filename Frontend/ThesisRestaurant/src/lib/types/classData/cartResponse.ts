export interface CartResponse {
    customFoods: Array<CartCustomFood>;
    foods: Array<CartFood>
}

export interface CartCustomFood {
    [key: string]: string | number | Array<CartIngredient>;
    guid: number;
    id: number;
    name: string;
    foodType: string;
    price: number;
    quantity: number;
    ingredients: Array<CartIngredient>
}

export interface CartFood {
    [key: string]: string | number | Array<CartIngredient> | Array<CartAdditionalIngredient>;
    guid: number;
    id: number;
    name: string;
    foodType: string;
    foodSizeId: number;
    foodSize: string;
    foodSizeMultiplier: number;
    price: number;
    quantity: number;
    ingredients: Array<CartIngredient>;
    additionalIngredients: Array<CartAdditionalIngredient>
}

export interface CartIngredient {
    name: string;
    ingredientType: string;
}

export interface CartAdditionalIngredient {
    id: number;
    name: string;
    ingredientType: string;
    price: number;
    quantity: number;
}