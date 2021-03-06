﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataNavigator {

	public static readonly string storagePath = Application.persistentDataPath + "/Music/";
	public static SongData currentSong;
	public static BeatmapBlueprint currentBlueprint;

	public static List<SongData> GetSongDataList() {
		List<SongData> songDataList = new List<SongData>();
		foreach (string songDirectoryPath in Directory.GetDirectories(storagePath)) {
			SongData songData = SongData.Load(songDirectoryPath);
			songDataList.Add(songData);
		}
		return songDataList;
	}

	public static SongData GetCurrentSongData() {
		if (currentSong == null)
			currentSong = GetSongDataList()[0];
		return currentSong;
	}

	public static BeatmapBlueprint GetCurrentBlueprint() {
		SongData song = GetCurrentSongData();
		if (song.blueprints.Count == 0)
			song.blueprints.Add(new BeatmapBlueprint());
		return song.blueprints[0];
	}
}
