namespace Orders.Application.Orders.DTOs.Responses
{
    public sealed class CancelOrderResponseDto
    {
            public int OrderId { get; init; }
            public DateTime CancelledDate { get; init; }
    }
}
