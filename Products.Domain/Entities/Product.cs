namespace Products.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; }
        public int? SupplierId { get; private set; }
        public int? CategoryId { get; private set; }
        public string? ProductName { get; private set; }
        public string? QuantityPerUnit { get; private set; }
        public int UnitsInStock { get; private set; }
        public int UnitsOnOrder { get; private set; }
        public int ReorderLevel { get; private set; }
        public bool Discontinued { get; private set; }
        public decimal UnitPrice { get; private set; }
        public Category Category { get; private set; }
        public Supplier Supplier { get; private set; }
    }
}
