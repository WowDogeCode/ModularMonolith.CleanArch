using Common.Application.DTOs;
using MediatR;
using Products.Application.Abstraction.Repositories;

namespace Products.Application.Products.GetProductsInventoryInfo
{
    public sealed class GetProductInventorySnapshotsQueryHandler : IRequestHandler<GetProductInventorySnapshotsQuery, List<ProductInventorySnapshotDto>>
    {
        private readonly IProductReadRepository _productReadRepository;
        public GetProductInventorySnapshotsQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<List<ProductInventorySnapshotDto>> Handle(GetProductInventorySnapshotsQuery request, CancellationToken cancellationToken)
        {
            var result = await _productReadRepository.GetProductInventorySnapshotsAsync(request.ProductIds, cancellationToken);

            return result;
        }
    }
}
