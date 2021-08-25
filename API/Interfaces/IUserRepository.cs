using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository 
    {
        // Requires using System.Collections.Generic;
        //  Requires using System.Threading.Tasks;
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        //  Gets users which should be of type IEnumerable
        Task<IEnumerable<AppUser>> GetUsersAsync();
        // Both Methods below are methods for gettings users by id or by username parameters. Task reprsents an async operation, async used in method name.
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<MemberDto> GetMemberAsync(string username);
    }
}