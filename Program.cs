
int counter = 0;
List<string> subdir = new List<string>();
List<string> fileExtensions = new List<string> { ".txt", ".pdf", ".docx", ".xlsx" };
var dirs = new Dictionary<string, string>{
    {".txt", "Text"},
    {".pdf", "Pdf"},
    {".docx", "Word"},
    {".xlsx", "Excel"}
};

Console.WriteLine("Enter the root directory: ");
var rootdir = Console.ReadLine().Replace("\"", "");

Console.WriteLine("Enter the destination directory: ");
var destdir = Console.ReadLine().Replace("\"", "");

var logDir = Path.Combine(rootdir, "Logs");
Directory.CreateDirectory(logDir);

GetSubdirectories(rootdir, subdir);
CreateDirsIfNotExist(destdir, dirs.Values);

var logPath = Path.Combine(logDir, $"DirectoryLog_{DateTime.Now:ddMMyyyy}.log");
using (StreamWriter sw = new StreamWriter(logPath, true))
{
    sw.WriteLine($"Start: {DateTime.Now}");
    try
    {
        foreach (string subdirectory in subdir)
        {
            foreach (var extension in fileExtensions)
            {
                var files = GetFilesByExtension(subdirectory, extension);
                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        var folderName = dirs[extension];
                        var destPath = Path.Combine(destdir, folderName);

                        var uniqueFilePath = Path.Combine(destPath, Path.GetFileName(file));
                        if (File.Exists(uniqueFilePath))
                        {
                            uniqueFilePath = Path.Combine(destPath, $"{Path.GetFileNameWithoutExtension(file)}_{Guid.NewGuid()}{extension}");
                        }

                        MoveFile(file, uniqueFilePath);
                        counter++;
                    }
                }
                else
                {
                    sw.WriteLine($"No files found with extension {extension} in {subdirectory}");
                }
            }
        }
        sw.WriteLine($"Total files moved: {counter}");
    }
    catch (Exception ex)
    {
        sw.WriteLine($"Exception: {ex}");
    }
    sw.WriteLine($"End: {DateTime.Now}");

}

void CreateDirsIfNotExist(string destDir, IEnumerable<string> directories)
{
    foreach (var dir in directories)
    {
        var path = Path.Combine(destDir+"\\DriveOrganizer", dir);
        Directory.CreateDirectory(path);
    }
}

List<string> GetFilesByExtension(string directory, string extension)
{
    var files = new List<string>();
    try
    {
        files.AddRange(Directory.GetFiles(directory, $"*{extension}"));
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception: {ex}");
    }
    return files;
}

void MoveFile(string source, string destination)
{
    try
    {
        File.Move(source, destination);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception: {ex}");
    }
}

void GetSubdirectories(string currentdir, List<string> subdirectories)
{
    try
    {
        string[] subDirs = Directory.GetDirectories(currentdir);
        subdirectories.AddRange(subDirs);

        foreach (string dir in subDirs)
        {
            GetSubdirectories(dir, subdirectories);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception: {ex}");
    }
}
