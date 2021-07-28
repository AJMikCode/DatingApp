using API.Entities;

namespace API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
        //The Token in which will be created as a string for each user being passed as the parameter. 
    }
}