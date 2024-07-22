namespace ExpirationDate.Models.Entitys
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
