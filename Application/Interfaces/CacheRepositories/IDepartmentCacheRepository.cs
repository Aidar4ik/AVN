using Domain.Entities.Catalog;

namespace Application.Interfaces.CacheRepositories
{

    // ПОтом в features доавить quiries GetCachedListAsync,GetByIdAsync
    public interface IDepartmentCacheRepository
    {
        Task<List<Department>> GetCachedListAsync();

        Task<Department> GetByIdAsync(int brandId);
    }
}
