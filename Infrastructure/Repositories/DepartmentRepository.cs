using Application.Interfaces.Repositories;
using Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IRepositoryAsync<Department> _repository;
        private readonly IDistributedCache _distributedCache;

        public DepartmentRepository(IDistributedCache distributedCache, IRepositoryAsync<Department> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Department> Departments => _repository.Entities;

        public async Task DeleteAsync(Department entity)
        {
            await _repository.DeleteAsync(entity);
            await _distributedCache.RemoveAsync(CacheKeys.DepartmentCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.DepartmentCacheKeys.GetKey(entity.Id));
        }

        public async Task<Department> GetByIdAsync(int entityId)
        {
            return await _repository.Entities.Where(p => p.Id == entityId).FirstOrDefaultAsync();
        }

        public async Task<List<Department>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Department entity)
        {
            await _repository.AddAsync(entity);
            await _distributedCache.RemoveAsync(CacheKeys.DepartmentCacheKeys.ListKey);
            return entity.Id;
        }

        public async Task UpdateAsync(Department entity)
        {
            await _repository.UpdateAsync(entity);
            await _distributedCache.RemoveAsync(CacheKeys.DepartmentCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.DepartmentCacheKeys.GetKey(entity.Id));
        }
    }
}
