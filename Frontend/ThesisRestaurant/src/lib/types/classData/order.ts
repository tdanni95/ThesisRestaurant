import type { CartResponse } from "./cartResponse";

export interface Order {
    [key: string]: string | number | Array<OrderResponse>;
    guid: number;
    user: string;
    items: Array<OrderResponse>;
}

export interface OrderResponse {
    [key: string]: string | number | CartResponse;
    id: number;
    address: string;
    created: string;
    items: CartResponse;
}