using Products.Domain.Abstraction;

namespace Products.Domain.Entities
{
    public class Product : IEntity
    {
        public int Id { get; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string ProductName { get; private set; }
        public string? QuantityPerUnit { get; private set; }
        public int? UnitsInStock { get; private set; }
        public int? UnitsOnOrder { get; private set; }
        public int? ReorderLevel { get; private set; }
        public bool Discontinued { get; private set; }
        public decimal? UnitPrice { get; private set; }
    }
}
