using MediatR;

namespace Products.Application.Products.AddProduct
{
    public record AddProductCommand : IRequest<int>
    {
        public int? SupplierId { get; init; }
        public int? CategoryId { get; init; }
        public string ProductName { get; init; } 
        public string? QuantityPerUnit { get; init; }
        public short UnitsInStock { get; init; }
        public short UnitsOnOrder { get; init; }
        public short ReorderLevel { get; init; }
        public bool Discontinued { get; init; }
        public decimal UnitPrice { get; init; }
    }
}