using System.ComponentModel.DataAnnotations;

namespace SulpakTest.Models
{
    public class LoginUser
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
