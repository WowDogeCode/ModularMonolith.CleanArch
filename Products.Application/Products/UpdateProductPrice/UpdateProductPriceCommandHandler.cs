using Common.Application.Abstraction;
using FluentValidation;
using MediatR;
using Products.Application.Abstraction.Repositories;
using Products.Application.Products.DTOs;

namespace Products.Application.Products.UpdateProductPrice
{
    public sealed class UpdateProductPriceCommandHandler : IRequestHandler<UpdateProductPriceCommand, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateProductPriceCommand> _validator;
        public UpdateProductPriceCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IValidator<UpdateProductPriceCommand> validator)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public async Task<ProductDto> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var product = await _productRepository.GetByIdAsync(request.ProductId);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            product.UpdatePrice(request.Price);
            await _unitOfWork.CommitAsync(cancellationToken);

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
