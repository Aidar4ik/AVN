using Application.Interfaces.CacheRepositories;
using Application.Interfaces.Repositories;
using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using Domain.Entities.Catalog;
using Infrastructure.CacheKeys;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.CacheRepositories
{
    public class FacultyCacheRepository : IFacultyCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IFacultyRepository _facultyRepository;

        public FacultyCacheRepository(IDistributedCache distributedCache, IFacultyRepository facultyRepository)
        {
            _distributedCache = distributedCache;
            _facultyRepository = facultyRepository;
        }

        public async Task<Faculty> GetByIdAsync(int brandId)
        {
            string cacheKey = FacultyCacheKeys.GetKey(brandId);
            var brand = await _distributedCache.GetAsync<Faculty>(cacheKey);
            if (brand == null)
            {
                brand = await _facultyRepository.GetByIdAsync(brandId);
                Throw.Exception.IfNull(brand, "Brand", "No Brand Found");
                await _distributedCache.SetAsync(cacheKey, brand);
            }
            return brand;
        }

        public async Task<List<Faculty>> GetCachedListAsync()
        {
            string cacheKey = FacultyCacheKeys.ListKey;
            var brandList = await _distributedCache.GetAsync<List<Faculty>>(cacheKey);
            if (brandList == null)
            {
                brandList = await _facultyRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, brandList);
            }
            return brandList;
        }
    }
}
