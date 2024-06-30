namespace DriveOrganizer3000.DirectoryUtils
{
	public class DirectoryUtil
	{
		public void CreateDirsIfNotExist(string destDir, IEnumerable<string> directories)
		{
			foreach(var dir in directories)
			{
				var path = Path.Combine($"{destDir}\\DriveOrganizer", dir);
				Directory.CreateDirectory(path);
			}
		}

		public void GetSubdirectories(string currentDir, List<string> subDirectories)
		{
			try
			{
				string[] subDirs = Directory.GetDirectories(currentDir);
				subDirectories.AddRange(subDirs);

				foreach(var dir in subDirs)
				{
					GetSubdirectories(dir, subDirectories);
				}
			}
			catch (Exception ex)
			{
                Console.WriteLine($"Error getting Subdirectories: {ex.Message}");
            }
		}
	}
}
