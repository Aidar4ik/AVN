using Application.Interfaces.CacheRepositories;
using Application.Interfaces.Repositories;
using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using Domain.Entities.Catalog;
using Infrastructure.CacheKeys;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.CacheRepositories
{
    public class DepartmentCacheRepository : IDepartmentCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentCacheRepository(IDistributedCache distributedCache, IDepartmentRepository departmentRepository)
        {
            _distributedCache = distributedCache;
            _departmentRepository = departmentRepository;
        }

        public async Task<Department> GetByIdAsync(int brandId)
        {
            string cacheKey = DepartmentCacheKeys.GetKey(brandId);
            var brand = await _distributedCache.GetAsync<Department>(cacheKey);
            if (brand == null)
            {
                brand = await _departmentRepository.GetByIdAsync(brandId);
                Throw.Exception.IfNull(brand, "Brand", "No Brand Found");
                await _distributedCache.SetAsync(cacheKey, brand);
            }
            return brand;
        }

        public async Task<List<Department>> GetCachedListAsync()
        {
            string cacheKey = DepartmentCacheKeys.ListKey;
            var brandList = await _distributedCache.GetAsync<List<Department>>(cacheKey);
            if (brandList == null)
            {
                brandList = await _departmentRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, brandList);
            }
            return brandList;
        }
    }
}
