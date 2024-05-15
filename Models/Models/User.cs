using Microsoft.AspNetCore.Identity;

namespace Models.Models
{
    public class User : IdentityUser
    {
        // Category properties ...............
        public string? Address { get; set; }

        // Relations .........................
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }
    }
}
