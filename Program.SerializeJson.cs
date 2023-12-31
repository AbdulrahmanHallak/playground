using System.Text.Json; // JsonSerializer
using static System.IO.Path;

namespace test;

partial class Program
{
    static async Task SerializeJsonAsync()
    {
        var person1 = new Person("John", "Doe", new DateTime(1980, 5, 15), new HashSet<Person>(), 75000.50m);
        var person2 = new Person("Jane", "Doe", new DateTime(1985, 8, 22), new HashSet<Person>(), 80000.75m);

        // Creating children for person1
        var child1 = new Person("Child1", "Doe", new DateTime(2010, 3, 10), new HashSet<Person>(), 0m);
        var child2 = new Person("Child2", "Doe", new DateTime(2012, 7, 5), new HashSet<Person>(), 0m);

        person1.Children.Add(child1);
        person1.Children.Add(child2);

        List<Person> people = [person1, person2];


        var options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            IncludeFields = true,
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };
        var path = Combine(Environment.CurrentDirectory, "people.json");
        using (FileStream stream = File.Create(path))
        {
            await JsonSerializer.SerializeAsync(utf8Json: stream, value: people, options);
        }

        WriteLine($"Written {new FileInfo(path).Length:NO} bytes of json to {path}");
        WriteLine();

        WriteLine(File.ReadAllText(path));
    }

    static async Task DeserializeJsonAsync()
    {
        WriteLine();
        WriteLine("* Deserializing JSON files");
        var path = Combine(Environment.CurrentDirectory, "people.json");
        using (FileStream jsonLoad = File.Open(path, FileMode.Open))
        {
            // deserialize object graph into a List of Person
            List<Person>? loadedPeople = await JsonSerializer.DeserializeAsync(utf8Json: jsonLoad, returnType: typeof(List<Person>)) as List<Person>;
            if (loadedPeople is not null)
            {
                foreach (Person p in loadedPeople)
                {
                    WriteLine("{0} has {1} children.",
                    p.LastName, p.Children?.Count ?? 0);
                }
            }
        }
    }
}