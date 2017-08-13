using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

public class MusicImporter {

	readonly string musicDirectory = Application.persistentDataPath + "/Music/";

	public MusicImporter () {
		if (!Directory.Exists (musicDirectory))
			Directory.CreateDirectory (musicDirectory);
	}

	public void ImportMusicFile (string inputFilePath) {
		string outputFilePath =  BuildOutputFilePath (inputFilePath);
		Debug.Log (inputFilePath);
		Debug.Log (outputFilePath);
		File.Copy (inputFilePath, outputFilePath);
	}

	string BuildOutputFilePath (string inputFilePath) {
		string outputFileName = Path.GetFileName (inputFilePath);
		StringBuilder stringBuilder = new StringBuilder ();
		stringBuilder.Append (musicDirectory);
		stringBuilder.Append (outputFileName);
		return stringBuilder.ToString ();
	}
}
