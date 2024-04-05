using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace WriteAndReadToCSV
{
    public static class ReadCsv
    {
        public static void ReadCsvfile()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };
            
            using (var reader = new StreamReader("filePersonsNoHeader.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<PersonMap>();

                var records = csv.GetRecords<Person>();

                foreach (var record in records) 
                {
                    Console.WriteLine($"{record.Id}, {record.Name}, {record.TaxPayer}, {record.DateOfBirth}");
                }
            }
        }
    }
}
