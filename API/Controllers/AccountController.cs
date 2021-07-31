using System.Security.Cryptography;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BasicApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]
        // The UserDto is what it expects so it expects to return public string Username and public string token.
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            // The if statement checks if the user exists and if they do it returns a bad request because that menas the usernmae has already been taken
            if (await UserExists(registerDto.Username)) return BadRequest("Username is Taken");

            // HMAC is a way to encrypt passwords to have a 128 letters, randomly generated key
            using var hmac = new HMACSHA512();

            //AppUser.cs get and set strings/byte []
            var user = new AppUser
            {
                //uses dto to set Usernmae in AppUser to registerDto
                UserName = registerDto.Username,
                //generates computehash, or hash value for the byte[]
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                //128 letter Key, different from Password Hash
                PasswordSalt = hmac.Key
            };
            //Adds the user using info typed into fields and generates PasswordHash and PasswordSalt.
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //returns the user added into DB, uses Dto properties and ITokenService to create tokenfor user
            return new UserDto 
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]

        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            //User variable sees if the username typed in is equal to the username within the database
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            //If user doesn't exist, will return null and if null unauthorized access to logging in
            if (user == null) return Unauthorized("Invalid username");

            //Uses key as Param for HMACSHA512, checks to see if Password Has is equal to Password Salt
            using var hmac = new HMACSHA512(user.PasswordSalt);

            //If password same as created in user and password salt is the same, user logs in.
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                //Goes thorugh DB of Byte [], if no values are equal to each other as in Password Hashes equaling each other, Return Invalid password
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            //return user if all is succesful in logging in.
            return new UserDto 
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
        private async Task<bool> UserExists(string username)
        {
            //Used Above and sees if the username exists in the DB and if it does, then the username returns invalid because cannot have two of the same username.
            return await _context.Users.AnyAsync(x => x.UserName == username);
        }
    }
}