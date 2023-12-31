using static System.Console;
using static System.IO.Path;
using System.Xml.Serialization;

namespace test;

partial class Program
{
    static void SerializeXml()
    {
        var person1 = new Person("John", "Doe", new DateTime(1980, 5, 15), new HashSet<Person>(), 75000.50m);
        var person2 = new Person("Jane", "Doe", new DateTime(1985, 8, 22), new HashSet<Person>(), 80000.75m);

        // Creating children for person1
        var child1 = new Person("Child1", "Doe", new DateTime(2010, 3, 10), new HashSet<Person>(), 0m);
        var child2 = new Person("Child2", "Doe", new DateTime(2012, 7, 5), new HashSet<Person>(), 0m);

        person1.Children.Add(child1);
        person1.Children.Add(child2);

        List<Person> people = [person1, person2];

        var xml = new XmlSerializer(typeof(List<Person>));

        var path = Combine(Environment.CurrentDirectory, "people.xml");
        using (FileStream stream = File.Create(path))
        {
            xml.Serialize(stream, people);
        }

        WriteLine($"Written {new FileInfo(path).Length:NO} bytes of xml to {path}");
        WriteLine();

        WriteLine(File.ReadAllText(path));

    }

    static void DeserializeXml()
    {
        WriteLine("Deserializing");

        var path = Combine(Environment.CurrentDirectory, "people.xml");
        using (FileStream stream = File.Open(path, FileMode.Open))
        {
            var des = new XmlSerializer(typeof(List<Person>));
            List<Person>? people = des.Deserialize(stream) as List<Person>;
            if (people is not null)
            {
                foreach (Person p in people)
                {
                    WriteLine("{0} has {1} children.",
                    p.LastName, p.Children?.Count ?? 0);
                }
            }
        }
    }
}

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public HashSet<Person> Children { get; set; }
    public decimal Salary { get; set; }
    public Person()
    { }
    public Person(string firstName, string lastName, DateTime dateOfBirth, HashSet<Person> children, decimal salary)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Children = children;
        Salary = salary;
    }
}