using Products.Domain.Abstraction;

namespace Products.Domain.Entities
{
    public sealed class Product : IEntity
    {
        public Product(int? supplierId, int? categoryId, string productName, string? quantityPerUnit, int? unitsInStock, int? unitsOnOrder, int? reorderLevel, bool discontinued, decimal? unitPrice)
        {
            SupplierId = supplierId;
            CategoryId = categoryId;
            ProductName = productName;
            QuantityPerUnit = quantityPerUnit;
            UnitsInStock = unitsInStock;
            UnitsOnOrder = unitsOnOrder;
            ReorderLevel = reorderLevel;
            Discontinued = discontinued;
            UnitPrice = unitPrice;
        }

        public int Id { get; private set; }
        public int? SupplierId { get; private set; }
        public int? CategoryId { get; private set; }
        public string ProductName { get; private set; }
        public string? QuantityPerUnit { get; private set; }
        public int? UnitsInStock { get; private set; }
        public int? UnitsOnOrder { get; private set; }
        public int? ReorderLevel { get; private set; }
        public bool Discontinued { get; private set; }
        public decimal? UnitPrice { get; private set; }

        public void DecreaseStock(int quantity)
        {
            if ((UnitsInStock ?? 0) < quantity)
            {
                throw new InvalidOperationException("Not enough stock to decrease");
            }
        }
    }
}
