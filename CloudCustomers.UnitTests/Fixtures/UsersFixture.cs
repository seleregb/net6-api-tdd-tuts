using System.Collections.Generic;
using CloudCustomers.API.Models;

namespace CloudCustomers.UnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<Users> GetTestUsers() =>
            new()
            {
                new Users
                {
                    Id = 1,
                    Name = "Jane",
                    Address = new Address()
                    {
                        Street = "123 Main Str",
                        City = "Halifax",
                        ZipCode = "B3H3K7"
                    },
                    Email = "jane@example.com"
                },
                new Users
                {
                    Id = 2,
                    Name = "Peter",
                    Address = new Address()
                    {
                        Street = "123 Main Str",
                        City = "Halifax",
                        ZipCode = "B3H3K7"
                    },
                    Email = "jane@example.com"
                },
                new Users
                {
                    Id = 3,
                    Name = "Paul",
                    Address = new Address()
                    {
                        Street = "123 Main Str",
                        City = "Halifax",
                        ZipCode = "B3H3K7"
                    },
                    Email = "jane@example.com"
                }
            };
    }
}