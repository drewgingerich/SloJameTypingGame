using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class SongImportManager {

	public static readonly string storagePath = Application.persistentDataPath + "/Music/";

	public static void ImportSong (string sourceFilePath) {
		string destinationPath = SongImportManager.storagePath + Path.GetFileName (sourceFilePath);
		File.Copy (sourceFilePath, destinationPath);

		string musicTrackTitle = Path.GetFileNameWithoutExtension (sourceFilePath);
		// string musicFileURL = "file://" + filePath;
		// AudioClip clip = SongClipReader.RetrieveClip (musicFileURL);

		SongPlayInfo playInfo = new SongPlayInfo ();
		playInfo.songTitle = musicTrackTitle;
		playInfo.songOffset = 0f;
		SongPlayInfoIO.SaveInfo(playInfo);
	}

	public static string[] GetImportedSongPaths () {
		string[] unfilteredFilePaths = Directory.GetFiles (storagePath);
		return unfilteredFilePaths.Where ( x => Path.GetExtension(x) == ".wav" ).ToArray ();
	}

	public static string[] GetImportedSongInfoPaths () {
		string[] unfilteredFilePaths = Directory.GetFiles (storagePath);
		return unfilteredFilePaths.Where ( x => Path.GetExtension(x) == ".xml" ).ToArray ();
	}

	public static string[] GetImportedSongTitles () {
		string[] songFilePaths = GetImportedSongPaths ();
		return songFilePaths.Select ( x => Path.GetFileNameWithoutExtension(x)).ToArray ();
	}

	public static void EnsureStorageDirectoryExists () {
		if (!Directory.Exists (storagePath))
			Directory.CreateDirectory (storagePath);
	}
}
