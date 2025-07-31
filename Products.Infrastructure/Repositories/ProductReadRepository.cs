using Dapper;
using Products.Application.Abstraction.Repositories;
using Products.Application.Products.Dtos;
using Products.Infrastructure.Utils;
using System.Data;

namespace Products.Infrastructure.Repositories
{
    public class ProductReadRepository : IProductReadRepository
    {
        private readonly IDbConnection _dbConnection;
        public ProductReadRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<List<ProductDto>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            var sql = SqlLoader.LoadSql("GetAllProducts.sql");

            var result = await _dbConnection.QueryAsync<ProductDto>(
                new CommandDefinition(sql, cancellationToken: cancellationToken));

            return result.AsList();
        }
    }
}
