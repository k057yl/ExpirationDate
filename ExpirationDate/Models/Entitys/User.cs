using Microsoft.AspNetCore.Identity;

namespace ExpirationDate.Models.Entitys
{
    public class User : IdentityUser
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
