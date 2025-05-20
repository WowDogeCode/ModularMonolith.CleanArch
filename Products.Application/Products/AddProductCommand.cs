using MediatR;

namespace Products.Application.Products
{
    public record AddProductCommand : IRequest<int>
    {
        public int? SupplierId { get; init; }
        public int? CategoryId { get; init; }
        public string ProductName { get; init; } 
        public string? QuantityPerUnit { get; init; }
        public int? UnitsInStock { get; init; }
        public int? UnitsOnOrder { get; init; }
        public int? ReorderLevel { get; init; }
        public bool Discontinued { get; init; }
        public decimal? UnitPrice { get; init; }
    }
}