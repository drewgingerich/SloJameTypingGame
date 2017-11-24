using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SongDataManager {

	public static readonly string storagePath = Application.persistentDataPath + "/Music/";

	public static List<SongData> GetSongDataList () {
		List<SongData> songDataList = new List<SongData> ();
		foreach (string songDirectoryPath in Directory.GetDirectories (storagePath)) {
			SongData songData = SongData.Load (songDirectoryPath);
			songDataList.Add (songData);
		}
		return songDataList;
	}
}
