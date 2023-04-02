import type { CartResponse } from "./cartResponse";

export interface Order {
    [key: string]: string | CartResponse;
    address: string;
    user: string;
    created: string;
    cart: CartResponse;
}