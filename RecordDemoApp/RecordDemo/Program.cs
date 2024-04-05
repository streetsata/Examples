using System.Text;

/*
 * Benefits of Records:
 *  - Simple to set up
 *  - Thread-safe
 *  - Easy/safe to share
 *  
 * When to use Records:
 *  - Capturing external Data that doesn't change - WeatherService, swapi.dev
 *  - API calls
 *  - Processing data
 *  - Read-only data
 *  
 * When not to use Records
 *  - When you need to change the data (Entity Framework)
 */

namespace RecordDemo
{
    internal class Program
    {


        protected Program()
        {
        }

        static void Main()
        {
            Record1 r1a = new(FirstName: "Serhii", LastName: "Artemenko");
            Record1 r1b = new(FirstName: "Serhii", LastName: "Artemenko");
            Record1 r1c = new(FirstName: "Oleksandr", LastName: "Artemenko");

            Class1 c1a = new(firstName: "Serhii", lastName: "Artemenko");
            Class1 c1b = new(firstName: "Serhii", lastName: "Artemenko");
            Class1 c1c = new(firstName: "Oleksandr", lastName: "Artemenko");

            Console.WriteLine("Record type:");
            Console.WriteLine($"To string {r1a}");
            Console.WriteLine($"Are the two objects equal: {Equals(r1a, r1b)}");
            Console.WriteLine($"Are the two objects reference equal: {ReferenceEquals(r1a, r1b)}");
            Console.WriteLine($"Are the two objects ==: {r1a == r1b}");
            Console.WriteLine($"Are the two objects !=: {r1a != r1b}");
            Console.WriteLine($"Hash code of object A: {r1a.GetHashCode()}");
            Console.WriteLine($"Hash code of object B: {r1b.GetHashCode()}");
            Console.WriteLine($"Hash code of object C: {r1c.GetHashCode()}");



            Console.WriteLine();
            Console.WriteLine(new string('*', 60));
            Console.WriteLine();

            Console.WriteLine("Class type:");
            Console.WriteLine($"To string {c1a}");
            Console.WriteLine($"Are the two objects equal: {Equals(c1a, c1b)}");
            Console.WriteLine($"Are the two objects reference equal: {ReferenceEquals(c1a, c1b)}");
            Console.WriteLine($"Are the two objects ==: {c1a == c1b}");
            Console.WriteLine($"Are the two objects !=: {c1a != c1b}");
            Console.WriteLine($"Hash code of object A: {c1a.GetHashCode()}");
            Console.WriteLine($"Hash code of object B: {c1b.GetHashCode()}");
            Console.WriteLine($"Hash code of object C: {c1c.GetHashCode()}");

            Console.WriteLine();

            var (fn, ln) = r1a;
            Console.WriteLine($"The value of fn is {fn}, the value of ln is {ln}");

            Record1 r1d = r1a with
            {
                FirstName = "Olena"
            };

            Console.WriteLine($"Olena's record: {r1d}");
            Console.WriteLine();

            Record2 r2a = new Record2(FirstName: "Serhii", LastName: "Artemenko");
            Console.WriteLine($"R2a Value: {r2a}");
            Console.WriteLine($"R2a fn: {r2a.FirstName}, ln: {r2a.LastName}");
            Console.WriteLine(r2a.SayHello());
        }
    }

    // a Record is just a fancy class
    // Immutable - The values cannot be changes
    public record Record1(string? FirstName, string? LastName);
    public record User1(string? Id, string FirstName, string LastName) : Record1(FirstName, LastName);
    public record Record2(string? FirstName, string? LastName)
    {
        private string? _firstName = FirstName;
        
        public string? FirstName
        {
            get => _firstName.Substring(0, 1);
            init { }
        }

        //public string? FirstName { get; init; } = FirstName;
        public string FullName { get => $"{FirstName} {LastName}"; }

        public string SayHello() => $"Hello, {FirstName}";
    }

    public class Class1
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }

        public Class1(string? firstName, string? lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void Decostruct(out string? FirstName, out string? LastName)
        {
            FirstName = this.FirstName;
            LastName = this.LastName;
        }
    }

    //****************************/
    // DO NOT DO ANY OF THE BELOW
    //****************************/
    public record Recor3 // No construct so no deconstruct
    {
        public string? FirstName { get; set; } // The set makes this record mutable
        public string? LasrName { get; set; }
    }

    // Don't just make clones all over to update state
}