export interface Address {
    [key: string]: string | number;
    id: number;
    zipCode: number;
    city: string;
    street: string;
    houseNumber: string;
}