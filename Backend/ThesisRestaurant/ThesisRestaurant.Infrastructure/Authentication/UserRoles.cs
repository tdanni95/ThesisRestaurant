namespace ThesisRestaurant.Infrastructure.Authentication
{
    internal static class UserRoles
    {
        public static readonly Dictionary<byte, string> Roles = new() {
            {1, "AppUser" },
            {2, "Employee"},
            {3, "Admin" }
        };
    }
}
