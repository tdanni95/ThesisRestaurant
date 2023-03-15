export interface RegisterErrors {
    [key: string | number]: string | number;
    Password: string;
    Email: string;
    FirstName: string;
    LastName: string;
    PhoneNumber: string;
}

export interface AddressError{
    [key: string | number]: string | number;
    ZipCode: string;
    City: string;
    Street: string;
    HouseNumber: string;
}