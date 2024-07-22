namespace ExpirationDate.Models.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<ItemDTO> Items { get; set; }
    }
}
