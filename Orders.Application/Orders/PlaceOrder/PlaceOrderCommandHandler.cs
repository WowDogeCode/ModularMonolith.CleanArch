using Common.Application.Abstraction;
using FluentValidation;
using MediatR;
using Orders.Application.Abstraction.Repositories;
using Orders.Domain.Entities;

namespace Orders.Application.Orders.PlaceOrder
{
    public sealed class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IValidator<PlaceOrderCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;
        public PlaceOrderCommandHandler(IOrderRepository orderRepository, IValidator<PlaceOrderCommand> validator, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            Order orderToAdd = new Order(
                employeeId: request.EmployeeId,
                customerId: request.CustomerId,
                shipVia: request.ShipVia,
                orderDate: request.OrderDate,
                requiredDate: request.RequiredDate,
                shippedDate: request.ShippedDate,
                freight: request.Freight,
                shipName: request.ShipName,
                shipAddress: request.ShipAddress,
                shipCity: request.ShipCity,
                shipRegion: request.ShipRegion,
                shipPostalCode: request.ShipPostalCode,
                shipCountry: request.ShipCountry
            );

            await _orderRepository.AddAsync(orderToAdd).ConfigureAwait(false);
            await _unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);

            return orderToAdd.Id;
        }
    }
}
