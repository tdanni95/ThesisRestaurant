using ErrorOr;

namespace ThesisRestaurant.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Users
        {
            public static Error DuplicateEmail => Error.Conflict(code: "User.DuplicateEmail",
                                                                 description: "Email is already in use");

            public static Error NotFound => Error.NotFound(code: "User.NotFound", description: "User not found");

            public static Error AddressNotFound => Error.NotFound(code: "UserAddress.NotFound", description: "Address not found");

            public static Error BadToken => Error.Validation(code: "User.InvdalitToken", description: "Token is invalud");
        }
    }
}
