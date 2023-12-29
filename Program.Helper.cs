namespace test;

partial class Program
{
    static void SectionTitle(string title)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.Cyan;
        WriteLine("*");
        WriteLine($"* {title}");
        WriteLine("*");
        ForegroundColor = previousColor;
    }

    static void WriteError(string message)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.Red;
        WriteLine("*");
        WriteLine($"* {message}");
        WriteLine("*");
        ForegroundColor = previousColor;
    }
    static void WriteWarning(string message)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.DarkYellow;
        WriteLine("*");
        WriteLine($"* {message}");
        WriteLine("*");
        ForegroundColor = previousColor;
    }
    static void WriteInformation(string message)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.Blue;
        WriteLine("*");
        WriteLine($"* {message}");
        WriteLine("*");
        ForegroundColor = previousColor;
    }
}