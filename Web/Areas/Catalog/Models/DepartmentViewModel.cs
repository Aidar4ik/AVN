using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Catalog.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentShortName { get; set; }
        public int FacultyId { get; set; }
        public SelectList Faculties { get; set; }
    }
}
