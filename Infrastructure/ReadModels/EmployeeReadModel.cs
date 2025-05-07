namespace Infrastructure.ReadModels
{
    public class EmployeeReadModel : IReadModel
    {
        public int Id { get; }
        public int? ReportsTo { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string? Title { get; }
        public string? TitleOfCourtesy { get; }
        public string? Address { get; }
        public string? City { get; }
        public string? Region { get; }
        public string? PostalCode { get; }
        public string? Country { get; }
        public string? HomePhone { get; }
        public string? Extension { get; }
        public string? Notes { get; }
        public string? PhotoPath { get; }
        public DateTime? BirthDate { get; }
        public DateTime? HireDate { get; }
    }
}
