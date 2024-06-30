using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace DriveOrganizer3000.Tests
{
	[TestFixture]
	public class ProgramTest
	{
		private string _rootDir;
		private string _destDir;

		[SetUp]
		public void Setup()
		{
			_rootDir = Path.Combine(Directory.GetCurrentDirectory(), "TestRoot");
			_destDir = Path.Combine(Directory.GetCurrentDirectory(), "TestDest");

			// Create test directories
			Directory.CreateDirectory(_rootDir);
			Directory.CreateDirectory(_destDir);

			// Create test files
			CreateTestFiles(_rootDir, ".txt", 3);
			CreateTestFiles(_rootDir, ".pdf", 2);
			CreateTestFiles(_rootDir, ".docx", 1);
			CreateTestFiles(_rootDir, ".xlsx", 4);
			CreateTestFiles(_rootDir, ".pptx", 0);
			CreateTestFiles(_rootDir, ".jpg", 2);
			CreateTestFiles(_rootDir, ".png", 1);
			CreateTestFiles(_rootDir, ".gif", 0);
		}

		[TearDown]
		public void Cleanup()
		{
			// Delete test directories and files
			Directory.Delete(_rootDir, true);
			Directory.Delete(_destDir, true);
		}

		[Test]
		public void TestFileOrganizer()
		{
			// Arrange
			var expectedFilesMoved = 13;

			// Act
			Program.Main(new string[] { _rootDir, _destDir });

			// Assert
			Assert.AreEqual(expectedFilesMoved, GetTotalFilesMoved());
		}

		private void CreateTestFiles(string directory, string extension, int count)
		{
			for (int i = 0; i < count; i++)
			{
				var filePath = Path.Combine(directory, $"TestFile{i}{extension}");
				File.Create(filePath).Close();
			}
		}

		private int GetTotalFilesMoved()
		{
			int totalFilesMoved = 0;
			var logDir = Path.Combine(_rootDir, "Logs");
			var logFiles = Directory.GetFiles(logDir, "DirectoryLog_*.log");

			foreach (var logFile in logFiles)
			{
				using (StreamReader sr = new StreamReader(logFile))
				{
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						if (line.StartsWith("Total files moved:"))
						{
							var filesMovedStr = line.Split(':')[1].Trim();
							totalFilesMoved += int.Parse(filesMovedStr);
						}
					}
				}
			}

			return totalFilesMoved;
		}
	}
}
