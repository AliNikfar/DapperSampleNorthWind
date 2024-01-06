
namespace DapperSampleNorthWind.Models.Contracts
{
    public interface IProductRepository
    {
        public Task<IEnumerable<ProductViewModel>> GetAllAsync();
        public Task InsertAsync (ProductViewModel model);
        public Task InsertBulkAsync(List<ProductViewModel> models);
        public Task InsertWithSPAsync (ProductViewModel model);
        public Task<int> InsertWithSPReturnsValue(ProductViewModel model);
        public Task UpdateAsync(ProductViewModel model);
        public Task UpdateBulkAsync(List<ProductViewModel> models);
        public Task<ProductViewModel> GetByIdAsync(int id);
        public Task DeleteAsync (int id);
        public Task DeleteBulkAsync(List<int> idList);
        public Task<(List<ProductViewModel>,List<CategoryViewModel>)> GetProductCategoryAsync();
    }
}
