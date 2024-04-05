using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace WriteAndReadToCSV
{
    public static class WriteCsv
    {
        public static void WritePersons()
        {
            var myPersons = new List<Person>()
            {
                new Person() { Id = 1, TaxPayer = true, Name = "Serhii", DateOfBirth = Convert.ToDateTime("15/08/1985", CultureInfo.GetCultureInfo("uk-UA").DateTimeFormat) },
                new Person() { Id = 2, TaxPayer = true, Name = "Olena", DateOfBirth = Convert.ToDateTime("11/08/2009", CultureInfo.GetCultureInfo("uk-UA").DateTimeFormat) },
                new Person() { Id = 3, TaxPayer = true, Name = "Sasha", DateOfBirth = Convert.ToDateTime("18/09/1992", CultureInfo.GetCultureInfo("uk-UA").DateTimeFormat) }
            };

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
                //Delimiter = ";",
                //Comment = 'S'
            };

            using (var stream = File.Open("filePersonsNoHeader.csv", FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(myPersons);
            }
        }
    }
}