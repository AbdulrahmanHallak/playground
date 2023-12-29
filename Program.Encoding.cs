using System.Text;
using static System.Console;

namespace test;

#pragma warning disable SYSLIB0001
partial class Program
{
    static void Encode()
    {
        WriteLine("Encodings");
        WriteLine("[1] ASCII");
        WriteLine("[2] UTF-7");
        WriteLine("[3] UTF-8");
        WriteLine("[4] UTF-16 (Unicode)");
        WriteLine("[5] UTF-32");
        WriteLine("[6] Latin1");
        WriteLine("[any other key] Default encoding");
        WriteLine();

        WriteLine("Press a number to choose an encoding");
        ConsoleKey key = ReadKey(intercept: true).Key;
        Encoding encoder = key switch
        {
            ConsoleKey.D1 or ConsoleKey.NumPad1 => Encoding.ASCII,
            ConsoleKey.D2 or ConsoleKey.NumPad2 => Encoding.UTF7,
            ConsoleKey.D3 or ConsoleKey.NumPad3 => Encoding.UTF8,
            ConsoleKey.D4 or ConsoleKey.NumPad4 => Encoding.Unicode,
            ConsoleKey.D5 or ConsoleKey.NumPad5 => Encoding.UTF32,
            ConsoleKey.D6 or ConsoleKey.NumPad6 => Encoding.Latin1,
            _ => Encoding.Default

        };

        var strToEncode = "Hello, World!";
        WriteLine($"Text to encode:     {strToEncode}");

        byte[] encoded = encoder.GetBytes(strToEncode);

        WriteLine("{0} used {1:N0} bytes.", encoder.GetType().Name, encoded.Length);
        WriteLine();
        // enumerate each byte
        WriteLine($"BYTE | HEX | CHAR");
        foreach (byte b in encoded)
        {
            WriteLine($"{b,4} | {b.ToString("X"),3} | {(char)b,4}");
        }
    }
}