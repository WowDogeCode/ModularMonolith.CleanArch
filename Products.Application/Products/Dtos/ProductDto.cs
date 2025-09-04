namespace Products.Application.Products.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string ProductName { get; set; } = default!;
        public string? QuantityPerUnit { get; set; }
        public int? UnitsInStock { get; set; }
        public int? UnitsOnOrder { get; set; }
        public int? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
