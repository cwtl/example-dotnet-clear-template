using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? CustomProp { get; set; }
    }
}
