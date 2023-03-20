namespace ThesisRestaurant.Contracts.User
{
    public record UserResponse
    (
        int Id, string FirstName, string LastName, string PhoneNumber, string Email, List<AddressResponse> Addresses
    );

   public record AddressResponse(
        int Id, int ZipCode, string City, string Street, string HouseNumber
       );
}
