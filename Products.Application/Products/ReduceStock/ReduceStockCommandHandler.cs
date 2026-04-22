using FluentValidation;
using MediatR;
using Products.Application.Abstraction.Repositories;
using Products.Domain.Entities;

namespace Products.Application.Products.ReduceStock
{
    public sealed class ReduceStockCommandHandler : IRequestHandler<ReduceStockCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<ReduceStockCommand> _validator;
        public ReduceStockCommandHandler(IProductRepository productRepository, IValidator<ReduceStockCommand> validator)
        {
            _productRepository = productRepository;
            _validator = validator;
        }

        public async Task<bool> Handle(ReduceStockCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            Product? product = await _productRepository.GetByIdAsNoTrackingAsync(request.ProductId, cancellationToken).ConfigureAwait(false);

            if (product != null)
            {
                var result = await _productRepository.TryDecrementStockAsync(request.ProductId, request.Quantity, cancellationToken).ConfigureAwait(false);

                return result;
            }

            return false;
        }
    }
}
