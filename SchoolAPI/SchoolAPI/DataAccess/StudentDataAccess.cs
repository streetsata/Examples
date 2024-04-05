using MongoDB.Driver;
using SchoolAPI.Interfaces;
using SchoolAPI.Models;

namespace SchoolAPI.DataAccess
{
    public class StudentDataAccess
    {
        private readonly IMongoCollection<Student> _students;
        private readonly IMongoCollection<Course> _courses;
        public StudentDataAccess(ISchoolDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _students = database.GetCollection<Student>(settings.StudentsCollectionName);
            _courses = database.GetCollection<Course>(settings.CoursesCollectionName);
        }
    }
}
