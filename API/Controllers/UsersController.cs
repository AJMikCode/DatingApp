using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //Authorize applies to all methods within this public class 
    [Authorize]
    public class UsersController : BasicApiController
    {
        //send in local variable to store it
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            //  Must return via Ok request with the users inside or else error of just returning 
            //  await request of not specifically returning a type required due to ActionResult keyword
            // In the end change GetUsersAsync to GetMembersAsync()
            var users = await _userRepository.GetMembersAsync();

            // Where were mapping to is MemberDto and passing in source object being users
            // When returning list of somehting such as list of users must return as Ienumerable
            //var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
            
            return Ok(users);
            // Can also be returned just with await request inside Ok request 
        }

        //will return route path of api/users/3. 3 being the id of the user.
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            //  No need to return in ok statement, dont know exactly why but maybe due to list/IEnumerable versus a single user being returned.
            return await _userRepository.GetMemberAsync(username);
        
            //return _mapper.Map<MemberDto>(user);
        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<AppUser>> GetUserById(int id)
        // {
        //     //  No need to return in ok statement, dont know exactly why but maybe due to list/IEnumerable versus a single user being returned.
        //     return await _userRepository.GetUserByIdAsync(id);
        // }
    }
}