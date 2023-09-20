using AspNetCoreHero.Abstractions.Domain;

namespace Domain.Entities.Catalog
{
    public class Faculty : AuditableEntity
    {
        public string FacultyName { get; set; }
        public string DeanName { get; set; }
        public string FacultyShortName { get; set; }
    }
}
