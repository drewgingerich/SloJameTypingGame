using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[System.Serializable]
public class SongData {

	public string directoryPath;
	public string audioFilePath;

	public string songTitle;
	public float bpm = 60f;
	public float beatTimingOffset;
	public float duration;
	public List<BeatmapBlueprint> blueprints;

	public SongData () {
		blueprints = new List<BeatmapBlueprint> ();
	}

	public SongData (string directoryPath, string songTitle) {
		this.directoryPath = directoryPath;
		this.songTitle = songTitle;
		blueprints = new List<BeatmapBlueprint> ();
	}

	public void Save () {
		string tempPath = directoryPath + "/~" + songTitle + ".xml~";
		string finalPath = directoryPath + "/" + songTitle + ".xml";
		XmlSerializer serializer = new XmlSerializer (typeof (SongData));
		using (FileStream stream = new FileStream (tempPath, FileMode.Create))
			serializer.Serialize(stream, this);
		File.Copy (tempPath, finalPath, true);
		File.Delete (tempPath);
	}

	public static SongData Load (string songDirectoyPath) {
		string songTitle = Path.GetFileName (songDirectoyPath);
		string songDataPath = Path.Combine (songDirectoyPath, songTitle + ".xml");
		if (!File.Exists (songDataPath)) {
			SongData newSongData = new SongData (songDirectoyPath, songTitle);
			newSongData.Save ();
		}
		XmlSerializer serializer = new XmlSerializer (typeof (SongData));
		using (FileStream stream = new FileStream (songDataPath, FileMode.Open))
			return (SongData)serializer.Deserialize (stream);
	}
}