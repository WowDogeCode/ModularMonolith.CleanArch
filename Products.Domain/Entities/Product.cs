using Products.Domain.Abstraction;

namespace Products.Domain.Entities
{
    public sealed class Product : IEntity
    {
        public Product(
            int? supplierId,
            int? categoryId,
            string productName,
            string? quantityPerUnit,
            decimal unitPrice = 0,
            short unitsInStock = 0,
            short unitsOnOrder = 0,
            short reorderLevel = 0,
            bool discontinued = false)
        {
            SupplierId = supplierId;
            CategoryId = categoryId;
            ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
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
        public short UnitsInStock { get; private set; }
        public short UnitsOnOrder { get; private set; }
        public short ReorderLevel { get; private set; }
        public bool Discontinued { get; private set; }
        public decimal UnitPrice { get; private set; }


        public void DecreaseStock(short quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero");
            }

            if (UnitsInStock < quantity)
            {
                throw new InvalidOperationException("Not enough stock to decrease");
            }

            UnitsInStock -= quantity;
        }
        public void UpdatePrice(decimal price)
        {
            UnitPrice = price;
        }

        public void UpdateStock(short stock)
        {
            UnitsInStock = stock;
        }
    }
}