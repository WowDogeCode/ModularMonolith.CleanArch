using Common.Application.Abstraction;
using FluentValidation;
using MediatR;
using Products.Application.Abstraction.Repositories;
using Products.Domain.Entities;

namespace Products.Application.Products.AddProduct
{
    public sealed class AddProductCommandHandler : IRequestHandler<AddProductCommand, int>
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<AddProductCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;
        public AddProductCommandHandler(IProductRepository productRepository, IValidator<AddProductCommand> validator, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            // TEMP: Manual validation; switch to pipeline behavior later
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            Product productToAdd = new Product(
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
            await _unitOfWork.CommitAsync().ConfigureAwait(false);

            return productToAdd.Id;
        }
    }
}
