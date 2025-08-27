using Common.Application.Abstraction;
using MediatR;
using Products.Application.Abstraction.Repositories;
using Products.Domain.Entities;

namespace Products.Application.Products.ReduceStock
{
    public sealed class ReduceStockCommandHandler : IRequestHandler<ReduceStockCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ReduceStockCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ReduceStockCommand request, CancellationToken cancellationToken)
        {
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
