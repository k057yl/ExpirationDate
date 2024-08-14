
namespace ExpirationDate.Models.DTO
{
    public class ItemDTO
    {        
        public int ItemId { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
    }
}