using Domain.Entities.BaseEntities;

namespace Domain.Entities.Catalog
{
    public class Department : AuditableEntity
    {
        public string DepartmentName { get; set; }
        public string DepartmentShortName { get; set; }

        public int FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }
    }
}
