using Dapper;
using DapperSampleNorthWind.Models.Contracts;

namespace DapperSampleNorthWind.Models.Implements
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly DapperContext _context;
        public SupplierRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SupplierViewModel>> GetSupplierForComboAsync()
        {
            var query = @"select SupplierID , CompanyName 
                          from dbo.Suppliers order by SupplierID";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<SupplierViewModel>(query);
                return result.ToList();
            }


        }
    }
}
