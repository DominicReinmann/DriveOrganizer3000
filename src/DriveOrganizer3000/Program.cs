

int counter = 0;
string rootdir, destDir, logDir = string.Empty;

List<string> subdir = new List<string>();
List<string> fileExtensions = new List<string> { ".txt", ".pdf", ".docx", ".xlsx", ".pptx", ".jpg", ".png", ".gif" };
var dirs = new Dictionary<string, string>
{
	{".txt", "Text"},
	{".pdf", "Pdf"},
	{".docx", "Word"},
	{".xlsx", "Excel"},
	{ ".pptx", "PowerPoint" },
	{".jpg", "Images"},
	{".png", "Images"},
	{".gif", "Images"}
};

while (true)
{
	// check if both are empty 
	if (string.IsNullOrEmpty(rootdir) && string.IsNullOrEmpty(destDir))
	{
		Console.WriteLine("Enter the root directory: ");
		rootdir = Console.ReadLine().Replace("\"", "");

		Console.WriteLine("Enter the destination directory: ");
		destDir = Console.ReadLine().Replace("\"", "");

		if (!string.IsNullOrEmpty(rootdir))
		{
			logDir = Path.Combine(rootdir, "Logs");
			Directory.CreateDirectory(logDir);
		}
	}
	else
	{
		if (string.IsNullOrEmpty(rootdir))
		{
			Console.WriteLine("Enter the root directory: ");
			rootdir = Console.ReadLine().Replace("\"", "");
		}

		if (string.IsNullOrEmpty(destDir))
		{
			Console.WriteLine("Enter the destination directory: ");
			destDir = Console.ReadLine().Replace("\"", "");
		}
	}
	if (!string.IsNullOrEmpty(rootdir) && !string.IsNullOrEmpty(destDir))
	{
		break;
	}
}

GetSubdirectories(rootdir, subdir);
CreateDirsIfNotExist(destDir, dirs.Values);

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
						var destPath = Path.Combine(destDir, folderName);

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