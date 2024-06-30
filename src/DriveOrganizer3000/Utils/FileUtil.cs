namespace DriveOrganizer3000.Utils
{
	public class FileUtil
	{

		public void MoveFile(string source, string destination)
		{
			try
			{
				File.Move(source, destination);
			}
			catch (Exception ex)
			{
                Console.WriteLine($"Error moving file: {ex.Message}");
            }
		}


		public List<string> GetFilesByExtension(string directory, string extension)
		{
			var files = new List<string>();
			try
			{
				files.AddRange(Directory.GetFiles(directory, $"*{extension}"));
				return files;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error getting file extension: {ex.Message}");
				return files;
			}
		}
	}
}
