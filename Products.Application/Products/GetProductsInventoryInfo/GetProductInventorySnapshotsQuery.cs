using Common.Application.DTOs;
using MediatR;

namespace Products.Application.Products.GetProductsInventoryInfo
{
    public record GetProductInventorySnapshotsQuery : IRequest<List<ProductInventorySnapshotDto>>
    {
        public List<int> ProductIds { get; init; } = new();
    }
}
