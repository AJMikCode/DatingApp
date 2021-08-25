using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using System.Linq;
using AutoMapper;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
        public async Task<bool> SaveAllAsync()
        {
            //Only Saves changes if the changes made are greater than 0
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            //  Just gets all values of users out of DB which contains all users
            return await _context.Users
                .Include(p => p.Photos)
                .ToListAsync();
        }
        // Both Methods below are methods for gettings users by id or by username parameters. Task reprsents an async operation, async used in method name.
        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            //  Just finds a set of value out of DB by id
            return await _context.Users.FindAsync(id);
        }
        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            //  Returns only one value with the specified username, if more than one, exception should be returned.
            return await _context.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            // Pretty much the same as GetMemberAsync()
                // Only Difference is return method at the end being .ToListAsync()
            return await _context.Users
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            //Without AutoMapper run this code, with string username as param for this method
                 return await _context.Users   // .Where() requires using System.Linq
                    .Where(x => x.UserName == username)
                        // Instead use ProjectTo<> AutoMapper method
                        // ProjectTo MemberDto and must pass param using automapper profiles or automapper configuration provider.
                    .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                    //.Select(user => new MemberDto)
                    //{
                        // Add MemberDto properties here
                        // Id = user.Id,
                        // Username = user.UserName
                        // About 20 properties just example shown of setting equal properties of user to MemberDto properties
                        //})
                    .SingleOrDefaultAsync();                
        }
    }
}