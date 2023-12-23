using Dapper;
using DapperSampleNorthWind.Models.Contracts;

namespace DapperSampleNorthWind.Models.Implements
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DapperContext _context;
        public CategoryRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CategoryViewModel>> GetCategoryForComboAsync()
        {
            var query = @"select categoryId , CategoryName
                          from dbo.Categories order by categoryId ";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<CategoryViewModel>(query);
                return result.ToList();
            }


        }
    }
}
