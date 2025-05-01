namespace Products.Domain.Entities
{
    public class Supplier
    {
        public int SupplierId { get; }
        public string CompanyName { get; private set; }
        public string? ContactName { get; private set; }
        public string? ContactTitle { get; private set; }
        public string? Address { get; private set; }
        public string? City { get; private set; }
        public string? Region { get; private set; }
        public string? PostalCode { get; private set; }
        public string? Country { get; private set; }
        public string? Phone { get; private set; }
        public string? Fax { get; private set; }
        public string? HomePage { get; private set; }
    }
}
