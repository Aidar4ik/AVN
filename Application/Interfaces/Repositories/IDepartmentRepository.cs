using Domain.Entities.Catalog;

namespace Application.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        IQueryable<Department> Departments { get; }

        Task<List<Department>> GetListAsync();

        Task<Department> GetByIdAsync(int departmentId);

        Task<int> InsertAsync(Department department);

        Task UpdateAsync(Department department);

        Task DeleteAsync(Department department);
    }
}
