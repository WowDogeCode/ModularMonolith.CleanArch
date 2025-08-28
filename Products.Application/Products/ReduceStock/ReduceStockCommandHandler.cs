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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<ReduceStockCommand> _validator;
        public ReduceStockCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IValidator<ReduceStockCommand> validator)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
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

                await _unitOfWork.CommitAsync(cancellationToken);

                return true;
            }

            return false;
        }
    }
}
