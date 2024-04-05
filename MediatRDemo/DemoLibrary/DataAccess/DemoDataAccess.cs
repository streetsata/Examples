using DemoLibrary.Models;

namespace DemoLibrary.DataAccess
{
    public class DemoDataAccess : IDataAccess
    {
        private List<PersonModel> people = new();
        public DemoDataAccess()
        {
            people.Add(new PersonModel() { Id = 1, FirstName = "Serhii", LastName = "Artemenko" });
            people.Add(new PersonModel() { Id = 2, FirstName = "Olena", LastName = "Artemenko" });
        }

        public List<PersonModel> GetPeople() => people;

        public PersonModel InsertPerson(string firstName, string lastName)
        {
            PersonModel p = new() { FirstName = firstName, LastName = lastName };
            p.Id = people.Max(x => x.Id) + 1;
            people.Add(p);

            return p;
        }
    }
}
