using Common.Application.Abstraction;
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
        private readonly IUnitOfWork _unitOfWork;
        public ReduceStockCommandHandler(IProductRepository productRepository, IValidator<ReduceStockCommand> validator, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ReduceStockCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            Product? product = await _productRepository.GetByIdAsync(request.ProductId).ConfigureAwait(false);

            if (product != null)
            {
                product.DecreaseStock(request.Quantity);
                await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);

                return true;
            }

            return false;
        }
    }
}
