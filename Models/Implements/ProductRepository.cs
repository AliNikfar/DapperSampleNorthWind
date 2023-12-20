using Dapper;

using DapperSampleNorthWind.Models;
using DapperSampleNorthWind.Models.Contracts;
using System.Data;
using System.Data.SqlClient;

namespace DapperSampleNorthWind.Models.Implements
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _config;
        private readonly IDbConnection _dbConnection;
        public ProductRepository(IConfiguration config, IDbConnection dbConnection)
        {
            _config = config;
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {


            var query = @"select prod.ProductID,prod.ProductName,prod.SupplierID,prod.CategoryID,
                          prod.QuantityPerUnit,prod.UnitPrice,cat.CategoryName , sup.CompanyName from
                          Products prod  join 
                          Categories cat on Prod.CategoryID = cat.CategoryID join 
                          Suppliers sup on prod.SupplierID = sup.SupplierID
                            ";

                return  await _dbConnection.QueryAsync<ProductViewModel>(query);
                




        }
    }
}
