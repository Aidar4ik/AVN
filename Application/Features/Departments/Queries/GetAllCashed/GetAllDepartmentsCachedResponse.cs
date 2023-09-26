namespace Application.Features.Departments.Queries.GetAllCashed
{
    public class GetAllDepartmentsCachedResponse
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentShortName { get; set; }

        public int FacultyId { get; set; }
    }
}
