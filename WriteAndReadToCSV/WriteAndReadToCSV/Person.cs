using CsvHelper.Configuration.Attributes;

namespace WriteAndReadToCSV
{
    public class Person
    {
        //[Name("Identifier")]
        [Index(0)]
        public int Id { get; set; }
        [Index(1)]
        public string? Name { get; set; }
        [Index(2)]
        public bool TaxPayer { get; set; }
        [Index(3)]
        public DateTime DateOfBirth { get; set; }
    }
}
