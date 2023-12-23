
namespace DapperSampleNorthWind.Models.Contracts
{
    public interface IProductRepository
    {
        public Task<IEnumerable<ProductViewModel>> GetAllAsync();
        public Task InsertAsync (ProductViewModel model);
        public Task InsertWithSPAsync (ProductViewModel model);
    }
}
