export interface Address {
    [key: string]: string | number;
    zipCode: number;
    city: string;
    street: string;
    houseNumber: string;
}