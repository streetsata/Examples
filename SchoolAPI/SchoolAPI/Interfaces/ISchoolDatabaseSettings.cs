namespace SchoolAPI.Interfaces
{
    public interface ISchoolDatabaseSettings
    {
        string ConnectionString { get; set; }
        string CoursesCollectionName { get; set; }
        string DatabaseName { get; set; }
        string StudentsCollectionName { get; set; }
    }
}