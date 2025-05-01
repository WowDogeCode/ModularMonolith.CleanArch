namespace Products.Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; }
        public string CategoryName { get; private set; }
        public string? Description { get; private set; }
        public ICollection<Product> Products { get; private set; } = new List<Product>();
    }
}
