namespace Infrastructure.ReadModels
{
    public class SupplierReadModel : IReadModel
    {
        public int Id { get; }
        public string CompanyName { get; }
        public string? ContactName { get; }
        public string? ContactTitle { get; }
        public string? Address { get; }
        public string? City { get; }
        public string? Region { get; }
        public string? PostalCode { get; }
        public string? Country { get; }
        public string? Phone { get; }
        public string? Fax { get; }
        public string? HomePage { get; }
    }
}
