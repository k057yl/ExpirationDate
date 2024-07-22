namespace ExpirationDate.Models.DTO
{
    public class ItemDTO
    {
        public int ItemId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
    }
}
