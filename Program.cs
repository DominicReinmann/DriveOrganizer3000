using System;
using System.IO;
using System.Collections.Generic;


string rootdir = "D:\\";
List<string> subdir = new List<string>();

GetSubdirectories(rootdir, subdir);
using(StreamWriter sw = new StreamWriter("C:\\var\\DirectoryLog.log", true))
{
foreach(string subdirectory in subdir){
	System.Console.WriteLine(subdirectory);
	LogWriter(sw, subdirectory);
}
}

static void GetSubdirectories(string currentdir, List<string> subdirectorie){
	try{
	string[] subDirs = Directory.GetDirectories(currentdir);
	subdirectorie.AddRange(subDirs);

	foreach(string fu in subDirs){
		GetSubdirectories(fu, subdirectorie);
	}
	}
	catch(Exception ex){
		System.Console.WriteLine($"Exception: {ex}");
	}
}

static void LogWriter(StreamWriter sw, string msg){
			sw.WriteLine(msg);
}
