using System.ComponentModel.DataAnnotations;

namespace NewsBlazor.Models
{
    public class UserCredentials
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
