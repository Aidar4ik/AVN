namespace Infrastructure.CacheKeys
{
    public static class FacultyCacheKeys
    {
        public static string ListKey => "FacultyList";

        public static string SelectListKey => "FacultySelectList";

        public static string GetKey(int facultyId) => $"Faculty-{facultyId}";

        public static string GetDetailsKey(int facultyId) => $"FacultyDetails-{facultyId}";
    }
}
