using Common.Application.Abstraction;
using FluentValidation;
using MediatR;
using Orders.Application.Abstraction.Repositories;
using Orders.Application.Orders.DTOs.Responses;
using Orders.Domain.Entities;

namespace Orders.Application.Orders.ShipOrder
{
    public sealed class ShipOrderCommandHandler : IRequestHandler<ShipOrderCommand, ShipOrderResponseDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<ShipOrderCommand> _validator;
        public ShipOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IValidator<ShipOrderCommand> validator)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<ShipOrderResponseDto> Handle(ShipOrderCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            Order? order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            order.ShipOrder(DateTime.UtcNow);
            await _unitOfWork.CommitAsync(cancellationToken);

            if (!order.ShippedDate.HasValue)
            {
                throw new Exception("Order was not shipped successfully");
            }

            return new ShipOrderResponseDto
            {
                ShippedDate = order.ShippedDate.Value
            };
        }
    }
}
