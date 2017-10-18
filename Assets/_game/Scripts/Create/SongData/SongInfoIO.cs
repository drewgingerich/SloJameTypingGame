using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public static class SongInfoIO {

	public static void SaveInfo (SongInfo playInfo) {
		string tempFile = SongImportManager.storagePath + "_temp.xml";
		XmlSerializer serializer = new XmlSerializer (typeof (SongInfo));
		using (FileStream stream = new FileStream (tempFile, FileMode.Create)) {
			serializer.Serialize(stream, playInfo);
		}
		string dataFile = SongImportManager.storagePath + playInfo.songTitle + ".xml";
		File.Copy (tempFile, dataFile, true);
		File.Delete (tempFile);
	}

	public static SongInfo LoadInfo (string trackName) {
		XmlSerializer serializer = new XmlSerializer (typeof (SongInfo));
		string dataPath = SongImportManager.storagePath + trackName + ".xml";
		SongInfo playInfo;
		if (File.Exists (dataPath)) {
			using (FileStream stream = new FileStream (dataPath, FileMode.Open)) {
				playInfo = (SongInfo)serializer.Deserialize (stream);
			}
		} else {
			playInfo = new SongInfo ();
			playInfo.songTitle = trackName;
			SaveInfo (playInfo);
		}
		return playInfo;
	}
}
