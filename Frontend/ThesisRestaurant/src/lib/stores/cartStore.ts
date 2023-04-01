import type { Cart, CartCustomItem, CartItem } from "$lib/types/cart";
import { writable } from "svelte/store";


function createCartStore() {
    const key = 'thesis-cart'
    const storedValue = typeof localStorage !== 'undefined' ? localStorage.getItem(key) : null
    const initialValue: Cart = storedValue ? JSON.parse(storedValue) : {
        customFoods: [] as Array<CartCustomItem>,
        foods: [] as Array<CartItem>
    }
    const { subscribe, update } = writable<Cart>(initialValue);

    function addItem(id: number, foodSizeId: number, additionalIngredients: Array<number> = []) {
        const cartItem: CartItem = {
            guid: Math.floor(Math.random() * 10000),
            id: id,
            foodSizeId: foodSizeId,
            additionalIngredients: additionalIngredients
        }

        update((cart) => ({
            customFoods: [...cart.customFoods],
            foods: [...cart.foods, cartItem]
        }))
    }

    function addCustomitem(id: number) {
        const cartCustomItem: CartCustomItem = {
            guid: Math.floor(Math.random() * 10000),
            id: id
        }
        update((cart) => ({
            customFoods: [...cart.customFoods, cartCustomItem],
            foods: [...cart.foods]
        }))
    }

    function deleteItem(guid: number, isCustom: boolean = false) {
        if (!isCustom) {
            update((cart) => ({
                customFoods: [...cart.customFoods],
                foods: cart.foods.filter((ci) => ci.guid !== guid)
            }))
        } else {
            update((cart) => ({
                customFoods: cart.customFoods.filter((cci) => cci.guid !== guid),
                foods: [...cart.foods]
            }))
        }
    }

    async function getCartValue(): Promise<Cart> {
        return new Promise((resolve) => {
            subscribe((cart) => {
                resolve(cart);
            })();
        });
      }

    if (typeof localStorage !== 'undefined') {
        // Save the latest value to local storage on every update
        subscribe((cart) => {
            localStorage.setItem(key, JSON.stringify(cart))
        })
    }


    return {
        subscribe,
        addItem: addItem,
        addCustomitem: addCustomitem,
        deleteItem: deleteItem,
        get: getCartValue
    }
}

export default createCartStore()