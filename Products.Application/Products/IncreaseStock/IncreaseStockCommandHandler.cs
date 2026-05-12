using FluentValidation;
using MediatR;
using Products.Application.Abstraction.Repositories;

namespace Products.Application.Products.IncreaseStock
{
    public sealed class IncreaseStockCommandHandler : IRequestHandler<IncreaseStockCommand, bool>
    {
        private readonly IValidator<IncreaseStockCommand> _validator;
        private readonly IProductRepository _productRepository;
        public IncreaseStockCommandHandler(IValidator<IncreaseStockCommand> validator, IProductRepository productRepository)
        {
            _validator = validator;
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(IncreaseStockCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);

            if (validationResult.IsValid is false)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var product = await _productRepository.GetByIdAsync(request.ProductId).ConfigureAwait(false);

            if (product is null)
            {
                return false;
            }

            product.IncreaseStock(request.Quantity);

            return true;
        }
    }
}
