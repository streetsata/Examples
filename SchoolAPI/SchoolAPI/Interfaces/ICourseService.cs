using SchoolAPI.Models;

namespace SchoolAPI.Interfaces
{
    public interface ICourseService
    {
        Task<Course?> Create(Course course);
        Task<Course?> GetById(string id);
    }
}
