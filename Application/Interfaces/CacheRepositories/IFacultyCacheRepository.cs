using Domain.Entities.Catalog;

namespace Application.Interfaces.CacheRepositories
{
    public interface IFacultyCacheRepository
    {
        Task<List<Faculty>> GetCachedListAsync();

        Task<Faculty> GetByIdAsync(int brandId);
    }
}
