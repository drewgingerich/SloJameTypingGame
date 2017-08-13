using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DirectoryDetective {

	string[] supportedExtensions = new string[3] {"mp3", "wav", "ogg"};
	string currentPath;
	public enum FileType {File, Directory};
	public DirectoryDetective (string startingPath) {
		currentPath = startingPath;
	}

	public void MoveUp () {
		currentPath = Directory.GetParent (currentPath).FullName;
	}

	public void MoveDown (string childPath) {
		FileAttributes attributes = File.GetAttributes (childPath);
		if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
			currentPath = childPath;
	}

	public SortedDictionary<string, FileType> GetDirectoryContents (string dirPath) {
		SortedDictionary<string, FileType> fileDict = new SortedDictionary<string, FileType> ();
		List<string> fileNames = GetFiles (dirPath);
		foreach (string filename in fileNames)
			fileDict.Add (filename, FileType.File);
		List<string> directoryNames = GetDirectories (dirPath);
		foreach (string dirname in directoryNames)
			fileDict.Add (dirname, FileType.Directory);
		return fileDict;
	}
	List<string> GetDirectories (string path) {
		return new List<string> (Directory.GetDirectories (path));
	}

	List<string> GetFiles (string path) {
		List<string> fileNames = new List<string> (Directory.GetFiles (path));
		foreach (string filename in fileNames) {
			if (!supportedExtensions.Any(Path.GetExtension(filename).Equals))
				fileNames.Remove(filename);
		}
		return fileNames;
	}
}
