using DapperSampleNorthWind.Models.Contracts;
using Dapper.Contrib.Extensions;
using Dapper;

namespace DapperSampleNorthWind.Models.Implements
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DapperContext _context;

        public OrderRepository(DapperContext context)
        {
            _context = context;
        }
        
        public async Task<List<OrderViewModel>> GetAllAsync()
        {
            using (var connection = _context.CreateConnection())
            {
                return  (await connection.GetAllAsync<OrderViewModel>()).ToList();
            }

        }

        public async Task<OrderViewModel> GetByIdAsync(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.GetAsync<OrderViewModel>(id);
            }
        }

        public async Task InserAsync(OrderViewModel model)
        {
            using (var connection = _context.CreateConnection())
            {
                await connection.InsertAsync<OrderViewModel>(model);
            }
        }
        public async Task<bool> UpdateAsync(OrderViewModel model)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.UpdateAsync<OrderViewModel>(model);
            }
        }
    }
}
