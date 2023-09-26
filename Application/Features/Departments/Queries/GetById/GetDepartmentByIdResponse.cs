namespace Application.Features.Departments.Queries.GetById
{
    public class GetDepartmentByIdResponse
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentShortName { get; set; }
        public int FacultyId { get; set; }
    }
}
