
namespace DapperSampleNorthWind.Models.Contracts
{
    public interface IProductRepository
    {
        public Task<IEnumerable<ProductViewModel>> GetAllAsync();
    }
}
