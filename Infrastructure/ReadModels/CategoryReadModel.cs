namespace Infrastructure.ReadModels
{
    public class CategoryReadModel : IReadModel
    {
        public int Id { get; }
        public string CategoryName { get; }
        public string? Description { get; }
    }
}
