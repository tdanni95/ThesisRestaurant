export interface RegisterErrors {
    [key: string]: string | number;
    Password: string;
    Email: string;
    FirstName: string;
    LastName: string;
    PhoneNumber: string;
}

export interface AddressError{
    [key: string]: string | number;
    ZipCode: string;
    City: string;
    Street: string;
    HouseNumber: string;
}