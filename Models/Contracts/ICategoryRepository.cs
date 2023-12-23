namespace DapperSampleNorthWind.Models.Contracts
{
    public interface ICategoryRepository
    {
         Task<IEnumerable<CategoryViewModel>> GetCategoryForComboAsync();
    }
}
