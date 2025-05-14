using Common.Application.Abstraction;

namespace Common.Application.ReadModels
{
    public class CustomerReadModel : IReadModel
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
    }
}
