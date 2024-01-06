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

        public async Task DeleteAsync(int id)
        {
            var query = ($@" delete  Products where ProductID = @ProductID ");

            using (var connection = _context.CreateConnection())
            {
                 await connection.ExecuteAsync(query, new { productId = id });

            }

        }

        public async Task DeleteBulkAsync(List<int> idList)
        {
            var query = ($@" delete  Products where ProductID in @idList  ");

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { idList = idList });

            }
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

        public async Task<ProductViewModel> GetByIdAsync(int id)
        {
            var query = ($@"select prod.ProductID,prod.ProductName,prod.SupplierID,prod.CategoryID,
                            prod.QuantityPerUnit,prod.UnitPrice,cat.CategoryName , sup.CompanyName from
                            Products prod  join 
                            Categories cat on Prod.CategoryID = cat.CategoryID join 
                            Suppliers sup on prod.SupplierID = sup.SupplierID where ProductID = @ProductID");

            using (var connection = _context.CreateConnection())
            {

                var result = await connection.QuerySingleOrDefaultAsync<ProductViewModel>(query,new { productId = id});
                return result;


            }
        }

        public async Task<(List<ProductViewModel>, List<CategoryViewModel>)> GetProductCategoryAsync()
        {
            var query = "select * from Products ; select * from categories";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryMultipleAsync(query);
                var products = await result.ReadAsync<ProductViewModel>();
                var categories = await result.ReadAsync<CategoryViewModel>();
                return (products.ToList(), categories.ToList());
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

        public async Task InsertBulkAsync(List<ProductViewModel> models)
        {
            var query = @"insert Products (ProductName,SupplierID,CategoryID,UnitPrice) " +
                         "values (@ProductName,@SupplierID,@CategoryID,@UnitPrice) ";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, models);
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
        public  async Task<int> InsertWithSPReturnsValue(ProductViewModel model)
        {
            var query = @"sp_products_Insert";
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("ProductName",model.ProductName);
                parameters.Add("SupplierID", model.SupplierID);
                parameters.Add("CategoryID", model.CategoryID);
                parameters.Add("UnitPrice", model.UnitPrice);
                parameters.Add("Id",DbType.Int32,direction: ParameterDirection.Output);
                await connection.ExecuteAsync(query,parameters, commandType: CommandType.StoredProcedure);
                int id = parameters.Get<int>("Id");
                return id;
            }
        }

        public async Task UpdateAsync(ProductViewModel model)
        {
            var query = @"update Products  set ProductName = @ProductName ,
                          SupplierID=@SupplierID,
                          CategoryID=@CategoryID,
                          UnitPrice=@UnitPrice 
                          where ProductID = @ProductId ";
            using (var connection = _context.CreateConnection())
            {
               await  connection.ExecuteAsync(query, model);
            }
        }

        public async Task UpdateBulkAsync(List<ProductViewModel> models)
        {
            var query = @"update Products  set ProductName = @ProductName ,
                          SupplierID=@SupplierID,
                          CategoryID=@CategoryID,
                          UnitPrice=@UnitPrice 
                          where ProductID = @ProductId ";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, models);
            }
        }
    }
}
