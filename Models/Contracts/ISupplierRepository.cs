namespace DapperSampleNorthWind.Models.Contracts
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<SupplierViewModel>> GetSupplierForComboAsync();
    }
}
