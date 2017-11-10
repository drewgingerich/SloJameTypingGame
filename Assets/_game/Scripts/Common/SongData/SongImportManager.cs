using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class SongImportManager {

	public static readonly string storagePath = Application.persistentDataPath + "/Music/";

	public static void ImportSong (string sourceFilePath) {
		string destinationPath = SongImportManager.storagePath + Path.GetFileName (sourceFilePath);
		File.Copy (sourceFilePath, destinationPath, true);
		string musicTrackTitle = Path.GetFileNameWithoutExtension (sourceFilePath);
		SongInfo songInfo = new SongInfo ();
		songInfo.songTitle = musicTrackTitle;
		SongInfoIO.SaveInfo(songInfo);
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
