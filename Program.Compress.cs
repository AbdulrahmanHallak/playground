using System.IO.Compression;
using System.Xml;

namespace test;

partial class Program
{
    static void Compress(string algorithm = "gzip")
    {
        var filePath = Path.Combine(Environment.CurrentDirectory, $"stream.{algorithm}");
        FileStream file = File.Create(filePath);
        Stream? compressor = null;

        if (algorithm == "gzip")
            compressor = new GZipStream(file, CompressionMode.Compress);

        else
            compressor = new BrotliStream(file, CompressionMode.Compress);
        using (compressor)
        {
            using (var writer = XmlWriter.Create(compressor))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("callsigns");

                foreach (var sign in Viper.Callsigns)
                    writer.WriteElementString("callsign", sign);
                writer.WriteEndElement();
            }
        }

        // decompress and read.
        file = File.Open(filePath, FileMode.Open);

        Stream decompressor;
        if (algorithm == "brotli")
        {
            decompressor = new BrotliStream(
              file, CompressionMode.Decompress);
        }
        else
        {
            decompressor = new GZipStream(
              file, CompressionMode.Decompress);
        }

        using (decompressor)
        {
            using (XmlReader reader = XmlReader.Create(decompressor))
            {
                while (reader.Read())
                {
                    // check if we are on an element node named callsign
                    if ((reader.NodeType == XmlNodeType.Element)
                      && (reader.Name == "callsign"))
                    {
                        reader.Read(); // move to the text inside element
                        WriteLine($"{reader.Value}"); // read its value
                    }
                }
            }
        }

    }
}