using CsvHelper.Configuration;

namespace WriteAndReadToCSV
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap() 
        {
            Map(p => p.Id).Index(0);
            Map(p => p.Name).Index(1);
            Map(p => p.TaxPayer).Index(2);
            Map(p => p.DateOfBirth).Index(3);
        }
    }
}
