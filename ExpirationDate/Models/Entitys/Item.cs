namespace ExpirationDate.Models.Entitys
{
    public class Item
    {
        public int ItemId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public bool NotificationSent { get; set; }
    }
}
