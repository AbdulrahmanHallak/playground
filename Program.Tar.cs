using System.Formats.Tar;
namespace test;

partial class Program
{
    static void ArchiveContent()
    {
        try
        {
            var current = Environment.CurrentDirectory;
            WriteInformation($"Current Directory:    {current}");

            var sourceDirectory = Path.Combine(current, "images");
            var destDirectory = Path.Combine(current, "extracted");
            var tarFile = Path.Combine(current, "images-archive.tar");

            if (!Directory.Exists(sourceDirectory))
            {
                WriteError($"The {sourceDirectory} directory must exist " +
                            "please create it and add some files to it");
                return;
            }
            if (File.Exists(tarFile))
            {
                File.Delete(tarFile);
                WriteWarning($"The {tarFile} already existed so it was deleted");
            }

            WriteInformation($"Archiving directory {sourceDirectory} to:\n{destDirectory}");

            TarFile.CreateFromDirectory(sourceDirectory, tarFile, true);


            WriteInformation($"Does {tarFile} exist? {File.Exists(tarFile)}.");
            if (!Directory.Exists(destDirectory))
            {
                // If the destination directory does not exist then we must create
                // it before extracting a Tar archive to it.
                Directory.CreateDirectory(destDirectory);
                WriteWarning($"{destDirectory} did not exist so it was created");

            }

            WriteInformation($"Extracting archive:{tarFile}\nTo directory: {destDirectory}");
            TarFile.ExtractToDirectory(
            sourceFileName: tarFile,
            destinationDirectoryName: destDirectory,
            overwriteFiles: true);
            if (Directory.Exists(destDirectory))
            {
                foreach (string dir in Directory.GetDirectories(destDirectory))
                {
                    WriteInformation(
                    $"Extracted directory {dir} containing these files: " +
                    string.Join(',', Directory.EnumerateFiles(dir)
                    .Select(file => Path.GetFileName(file))));
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}