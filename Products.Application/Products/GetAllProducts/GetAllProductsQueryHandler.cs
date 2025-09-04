using MediatR;
using Products.Application.Abstraction.Repositories;
using Products.Application.Products.DTOs;

namespace Products.Application.Products.GetAllProducts
{
    public sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly IProductReadRepository _productReadRepository;
        public GetAllProductsQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productsList = await _productReadRepository.GetAllProductsAsync(cancellationToken).ConfigureAwait(false);

            return productsList;
        }
    }
}
