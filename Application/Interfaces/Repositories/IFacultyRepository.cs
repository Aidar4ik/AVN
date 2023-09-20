using Domain.Entities.Catalog;

namespace Application.Interfaces.Repositories
{
    public interface IFacultyRepository
    {
        IQueryable<Faculty> Faculties { get; }

        Task<List<Faculty>> GetListAsync();

        Task<Faculty> GetByIdAsync(int facultyId);

        Task<int> InsertAsync(Faculty faculty);

        Task UpdateAsync(Faculty faculty);

        Task DeleteAsync(Faculty faculty);
    }
}
