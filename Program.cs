using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;


List<string> subdir = new List<string>();
List<string> fileExtension += new List<string> { ".txt", ".pdf", ".docx", "xlsx" };
var dirs += new List<string> { "", "", }
var timer = new Stopwatch();


Console.WriteLine("Enter the root directory: ");\
var rootdir = Console.ReadLine();

Console.WriteLine("Enter the destination directory: ");
var destdir = Console.ReadLine();

var logDir = $"{rootdir}\\Logs\\";

GetSubdirectories(rootdir, subdir);
CreateDirIfNotExist(destdir);
using (StreamWriter sw = new StreamWriter($"{logDir}DirectoryLog_{DateOnly}.log", true))
{
    sw.WriteLine($"Start: {DateTime.Now}");
    try
    {
        foreach (string subdirectory in subdir)
        {
            var files = new List<string>();
            foreach (var extension in fileExtension)
            {
                if (files.Any())
                {
                    GetFileExtension(subdirectory, extension, files);
                    foreach (var file in files)
                    {
                        MoveFile(file, $"{destdir}\\{extension}";
                    }
                }
                else
                {
                    sw.WriteLine($"No files found with extension {extension} in {subdirectory}");
                }
            }
        }
    }
    catch (Exception ex)
    {
        sw.WriteLine($"Exception: {ex}");
    }
    sw.WriteLine($"End: {DateTime.Now}");
}
public void CreateDirIfNotExist(string destDir)
{
    try
    {
        foreach (var fu in dirs)
        {
            Directory.CreateDirectory($"{destDir}\\fu");
        }
    }
    catch (Exception ex)
    {
        System.Console.WriteLine($"Exception: {ex}");
    }
}

public void GetFileExtension(string directory, string extension, List<string> files)
{
    try
    {
        string[] fileEntries = Directory.GetFiles(directory);
        foreach (string fileName in fileEntries)
        {
            if (fileName.EndsWith(extension))
            {
                files.Add(fileName);
            }
        }
    }
    catch (Exception ex)
    {
        System.Console.WriteLine($"Exception: {ex}");
    }
}

public void MoveFile(string source, string destination)
{
    try
    {
        File.Move(source, destination);
    }
    catch (Exception ex)
    {
        System.Console.WriteLine($"Exception: {ex}");
    }
}


static void GetSubdirectories(string currentdir, List<string> subdirectorie)
{
    try
    {
        string[] subDirs = Directory.GetDirectories(currentdir);
        subdirectorie.AddRange(subDirs);

        foreach (string fu in subDirs)
        {
            GetSubdirectories(fu, subdirectorie);
        }
    }
    catch (Exception ex)
    {
        System.Console.WriteLine($"Exception: {ex}");
    }
}

static void LogWriter(StreamWriter sw, string msg)
{
    sw.WriteLine(msg);
}


