using System.Xml;
using static System.Console;
using static System.IO.Directory;
using static System.IO.Path;

namespace test;

partial class Program
{
    static void WriteXmlStream()
    {
        var dir = Combine(Environment.CurrentDirectory, "streams.xml");

        FileStream? xmlStream = null;
        XmlWriter? xmlWriter = null;

        try
        {
            xmlStream = File.Create(dir);
            xmlWriter = XmlWriter.Create(xmlStream, new XmlWriterSettings { Indent = true });

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("callsigns");
            foreach (var sign in Viper.Callsigns)
                xmlWriter.WriteElementString("callsign", sign);
            xmlWriter.WriteEndElement();

            xmlWriter.Close();
            xmlStream.Close();
        }
        finally
        {
            if (xmlStream is not null) xmlStream.Dispose();
            if (xmlWriter is not null) xmlWriter.Dispose();
        }

        WriteLine(File.ReadAllText(dir));
    }
}