using Dapper;

using DapperSampleNorthWind.Models;
using DapperSampleNorthWind.Models.Contracts;
using System.Data;
using System.Data.SqlClient;

namespace DapperSampleNorthWind.Models.Implements
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperContext _context;
        public ProductRepository( DapperContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {

            var query = @"select prod.ProductID,prod.ProductName,prod.SupplierID,prod.CategoryID,
                            prod.QuantityPerUnit,prod.UnitPrice,cat.CategoryName , sup.CompanyName from
                            Products prod  join 
                            Categories cat on Prod.CategoryID = cat.CategoryID join 
                            Suppliers sup on prod.SupplierID = sup.SupplierID ";

                using  (var connection = _context.CreateConnection())
                {

                var result = await connection.QueryAsync<ProductViewModel>(query);
                return result.ToList();


                }

        }

        public async Task InsertAsync(ProductViewModel model)
        {
            var query = @"insert Products (ProductName,SupplierID,CategoryID,UnitPrice) " +
                "values (@ProductName,@SupplierID,@CategoryID,@UnitPrice) ";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query,model);
            }


        }
        public async Task InsertWithSPAsync(ProductViewModel model)
        {
            var query = @"sp_products_Insert";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query,new { ProductName = model.ProductName, SupplierID=model.SupplierID,
                CategoryID = model.CategoryID ,UnitPrice = model.UnitPrice},commandType:CommandType.StoredProcedure);
            }
        }

    }
}
