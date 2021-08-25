using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class MemberDto
    {
        //  All commas are changes from the copied AppUser.cs
        public int Id { get; set; }
        public string UserName { get; set; }
        //  Remove passwordhash and passwordsalt, copied from AppUser.cs
        //  Change DateTime DateOfBirth to int Age
        public string PhotoUrl {get; set;}
        public int Age { get; set; }
        public string Nickname { get; set; }
        //  Remover setting equal to DateTime.Now initializers
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; } 
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        //Change from Photo.cs to a Dto since Photo class didnt work.
        //  Create a PhotoDto.cs with info from same 
      public ICollection<PhotoDto> Photos { get; set; }
    }
}