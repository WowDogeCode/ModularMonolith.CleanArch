using Common.Application.DTOs;
using Dapper;
using Products.Application.Abstraction.Repositories;
using Products.Application.Products.DTOs;
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
            var sql = SqlLoader.LoadSql("GetAllProductsAsync.sql");

            var result = await _dbConnection.QueryAsync<ProductDto>(
                new CommandDefinition(sql, cancellationToken: cancellationToken));

            return result.AsList();
        }
        public async Task<List<ProductInventorySnapshotDto>> GetProductInventorySnapshotsAsync(List<int> productIds, CancellationToken cancellationToken)
        {
            var sql = SqlLoader.LoadSql("GetInventoryByProductIdsAsync.sql");

            var result = await _dbConnection.QueryAsync<ProductInventorySnapshotDto>(
                new CommandDefinition(
                    sql, 
                    new { ProductIds = productIds }, 
                    cancellationToken: cancellationToken));

            return result.AsList();
        }
    }
}
