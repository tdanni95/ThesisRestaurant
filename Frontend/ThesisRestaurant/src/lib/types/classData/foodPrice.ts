export interface FoodPrice{
    [key: string]: string | number;
    id: number;
    from: string;
    to: string;
    price: number;
}