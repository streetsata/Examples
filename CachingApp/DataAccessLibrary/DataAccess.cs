using Microsoft.Extensions.Caching.Memory;

namespace DataAccessLibrary
{
    public class DataAccess
    {
        private readonly IMemoryCache _memoryCache;

        public DataAccess(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> output = new();

            output.Add(new() { FirstName = "Serhii", LastName = "Artemenko" });
            output.Add(new() { FirstName = "Olena", LastName = "Artemenko" });
            output.Add(new() { FirstName = "Sasha", LastName = "Artemenko" });

            Thread.Sleep(3000);

            return output;
        }

        public async Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            List<EmployeeModel> output = new();

            output.Add(new() { FirstName = "Serhii", LastName = "Artemenko" });
            output.Add(new() { FirstName = "Olena", LastName = "Artemenko" });
            output.Add(new() { FirstName = "Sasha", LastName = "Artemenko" });

            await Task.Delay(3000);

            return output;
        }

        public async Task<List<EmployeeModel>> GetCachedEmloyeesAsync()
        {
            List<EmployeeModel>? output;

            output = _memoryCache.Get<List<EmployeeModel>>(key: "employees") ?? null;

            if (output is null)
            {
                output = new();

                output.Add(new() { FirstName = "Serhii", LastName = "Artemenko" });
                output.Add(new() { FirstName = "Olena", LastName = "Artemenko" });
                output.Add(new() { FirstName = "Sasha", LastName = "Artemenko" });

                await Task.Delay(3000);

                _memoryCache.Set("employees", output, TimeSpan.FromMinutes(1));
            }

            return output;
        }
    }
}
