namespace test;

partial class Program
{
    static async Task Main(string[] args)
    {
        SectionTitle("* Changing Terminal Color");

        // WriteXmlStream();

        // Compress();
        // Compress("brotli");
        // ArchiveContent();

        // Encode();
        // SerializeXml();
        await SerializeJsonAsync();
        // await DeserializeJsonAsync();
    }
}
