using MediatR;
using Products.Application.Abstraction.Repositories;
using Products.Domain.Entities;

namespace Products.Application.Products
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, int>
    {
        private readonly IProductRepository _productRepository;
        public AddProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var productToAdd = new Product(
                supplierId: request.SupplierId,
                categoryId: request.CategoryId,
                productName: request.ProductName,
                quantityPerUnit: request.QuantityPerUnit,
                unitsInStock: request.UnitsInStock,
                unitsOnOrder: request.UnitsOnOrder,
                reorderLevel: request.ReorderLevel,
                discontinued: request.Discontinued,
                unitPrice: request.UnitPrice
            );

            await _productRepository.AddAsync(productToAdd).ConfigureAwait(false);

            return productToAdd.Id;
        }
    }
}
