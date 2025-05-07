namespace Infrastructure.ReadModels
{
    public class ShipperReadModel : IReadModel
    {
        public int Id { get; }
        public string CompanyName { get; }
        public string? Phone { get; }
    }
}
