using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        //SymmetricSecurityKey requires using Microsoft.IdentityModel.Tokens
        //One key used to encrypt and decrypt electronic information.
        //AsymmetricSecurityKeys, one public and one private used ot encrypt and decrypt messages.
        //Key no need to go anywhere for jwt so use SymmetricSecurityKey
        private readonly SymmetricSecurityKey _key;
        //IConfiguration requires using Microsoft.Extensions.Configuration
        // This public TokenService() is a constructor
        public TokenService(IConfiguration config)
        {
            //byte[] as parameter of SymmetricSecurityKey
            //Encoding keyword requires using System.Text;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(AppUser user)
        {
            //List requires using System.Security.Claims; <Claims> and System.Collections.Generic;
            var claims = new List<Claim>
            {
                //JwtRegisteredClaimNames requires using System.IdentityModel.Tokens.Jwt;
                //Claim Method uses JwtRegisteredClaimNames.NameId to store user.UserName. Doing lots of things using usernmae stoed in jwt.
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };
                // Params of method are security key and algorithim, most secure is HMACSHA512
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor 
            {
                //Claims Identity and SigningCredentials are methods that come from the metadata library of the using Microsoft.IdentityModel.Tokens
                //DateTime.Now is part of the using System library.
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
        //     Gets or sets the property name of System.Security.Claims.Claim.Properties the
        //     will contain .Net type that was recognized when JwtPayload.Claims serialized
        //     the value to JSON. Just needed no real coding logic to it.
            var tokenHandler = new JwtSecurityTokenHandler();

            //Creates the Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //Return Written token to whoever needs it.
            return tokenHandler.WriteToken(token);
        }
    }

}