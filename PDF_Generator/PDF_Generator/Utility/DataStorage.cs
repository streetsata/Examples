using PDF_Generator.Models;

namespace PDF_Generator.Utility
{
    public static class DataStorage
    {
        public static List<Employee> GetAllEmployees() =>
            new List<Employee>()
            {
                new Employee { Name = "Serhii", LastName = "Artemenko", Gender = "Male", Age = 38 },
                new Employee { Name = "Olena", LastName = "Artemenko", Gender = "Female", Age = 31 },
                new Employee { Name = "Sasha", LastName = "Artemenko", Gender = "Male", Age = 14 },
                new Employee { Name = "Ivanna", LastName = "Ivanova", Gender = "Female", Age = 25 }
            };
    }
}
