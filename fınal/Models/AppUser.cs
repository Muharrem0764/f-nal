using Microsoft.AspNetCore.Identity;

namespace fınal.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string City { get; set; }
        public string PhotoUrl { get; internal set; }
    }
}
