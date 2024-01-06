namespace DapperSampleNorthWind.Models.Contracts
{
    public interface IOrderRepository
    {
        Task<List<OrderViewModel>> GetAllAsync();
        Task<OrderViewModel> GetByIdAsync(int id);
        Task InserAsync(OrderViewModel model);
        Task<bool> UpdateAsync(OrderViewModel model);
    }
}
