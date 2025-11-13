using Common.Application.Abstraction;
using FluentValidation;
using MediatR;
using Products.Application.Abstraction.Repositories;
using Products.Application.Products.DTOs;

namespace Products.Application.Products.UpdateProductStock
{
    public sealed class UpdateProductStockCommandHandler : IRequestHandler<UpdateProductStockCommand, ProductDto>
    {
        private readonly IValidator<UpdateProductStockCommand> _validator;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProductStockCommandHandler(IValidator<UpdateProductStockCommand> validator, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ProductDto> Handle(UpdateProductStockCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var product = await _productRepository.GetByIdAsync(request.ProductId).ConfigureAwait(false);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            product.UpdateStock(request.Stock);

            await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);

            return new ProductDto
            {
                CategoryId = product.CategoryId,
                ProductName = product.ProductName,
                Discontinued = product.Discontinued,
                QuantityPerUnit = product.QuantityPerUnit,
                ReorderLevel = product.ReorderLevel,
                SupplierId = product.SupplierId,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder
            };
        }
    }
}
