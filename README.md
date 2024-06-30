# Drive Organizer

This is a simple console application that organizes files in a given directory based on their file extensions. It moves files to corresponding subdirectories based on their file types.

## Usage

1. Clone the repository or download the source code.

2. Open the solution in Visual Studio.

3. Build the solution to compile the code.

4. Run the application.

5. Enter the root directory path when prompted. This is the directory where the files to be organized are located.

6. Enter the destination directory path when prompted. This is the directory where the organized files will be moved to.

7. The application will create subdirectories in the destination directory based on the file extensions and move the files accordingly.

8. After the process is complete, a log file will be generated in the root directory under the "Logs" folder. The log file will contain information about the files moved and any exceptions encountered during the process.

## Configuration

The application is configured to organize files with the following extensions:

- .txt
- .pdf
- .docx
- .xlsx
- .pptx
- .jpg
- .png
- .gif

You can modify the list of file extensions and their corresponding subdirectories in the `dirs` dictionary in the `Program.cs` file.
