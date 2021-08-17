using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set;}

        [Required]
        //  Another way to get response out of postman for error handling section
        [StringLength(8, MinimumLength = 4)] 
        public string Password { get; set; }
    }
}