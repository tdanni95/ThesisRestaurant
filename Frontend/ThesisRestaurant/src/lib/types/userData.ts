import type { Address } from "./address";

export interface UserData {
    [key: string | number]: string | number | Array<Address>;
    firstName: string;
    lastName: string;
    email: string;
    role: string;
    phoneNumber: string;
    level: number;
    id: number;
    addresses: Array<Address>;
}