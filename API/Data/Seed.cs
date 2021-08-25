using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        // No type of Task returning technically void, still has async functionality
        public static async Task SeedUsers(DataContext context)
        {
            // If any users, return the Users 
            if (await context.Users.AnyAsync()) return;

            //uses json files of fake users for seeding data into db.
            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            //Deserialize string of json into object
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            //Add into database ad register component did
            foreach (var user in users) 
            {
                //Kind of like the AccountController with adding values
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                //Sets password as the test, trying to see what app looks like from User perspective.
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                // Works like the account controller by adding user once AppUse info s filled out including the userName, PasswordHash, PasswordSalt.
                context.Users.Add(user);
            }
            await context.SaveChangesAsync();
        }
    }
}