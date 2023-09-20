using Application.Interfaces.Repositories;
using Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Repositories
{
    public class FacultyRepository : IFacultyRepository
    {
        private readonly IRepositoryAsync<Faculty> _repository;
        private readonly IDistributedCache _distributedCache;

        public FacultyRepository(IDistributedCache distributedCache, IRepositoryAsync<Faculty> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Faculty> Faculties => _repository.Entities;

        public async Task DeleteAsync(Faculty entity)
        {
            await _repository.DeleteAsync(entity);
            await _distributedCache.RemoveAsync(CacheKeys.FacultyCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.FacultyCacheKeys.GetKey(entity.Id));
        }

        public async Task<Faculty> GetByIdAsync(int entityId)
        {
            return await _repository.Entities.Where(p => p.Id == entityId).FirstOrDefaultAsync();
        }

        public async Task<List<Faculty>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Faculty entity)
        {
            await _repository.AddAsync(entity);
            await _distributedCache.RemoveAsync(CacheKeys.FacultyCacheKeys.ListKey);
            return entity.Id;
        }

        public async Task UpdateAsync(Faculty entity)
        {
            await _repository.UpdateAsync(entity);
            await _distributedCache.RemoveAsync(CacheKeys.FacultyCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.FacultyCacheKeys.GetKey(entity.Id));
        }
    }
}
