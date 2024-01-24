

using Microsoft.AspNetCore.Identity;

namespace IdentityUser100.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public ICollection<UserProperty>? UserProperties { get; set; }
    }
}
