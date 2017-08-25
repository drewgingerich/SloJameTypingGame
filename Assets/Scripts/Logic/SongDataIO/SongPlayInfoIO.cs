using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public static class SongPlayInfoIO {


	public static void SaveInfo (SongPlayInfo playInfo) {
		XmlSerializer serializer = new XmlSerializer (typeof (SongPlayInfo));
		string dataPath = SongImportManager.storagePath + playInfo.songTitle + ".dat";
		using (FileStream stream = new FileStream (dataPath, FileMode.OpenOrCreate)) {
			serializer.Serialize(stream, playInfo);
		}
	}

	public static SongPlayInfo LoadInfo (string trackName) {
		XmlSerializer serializer = new XmlSerializer (typeof (SongPlayInfo));
		string dataPath = SongImportManager.storagePath + trackName + ".dat";
		SongPlayInfo playInfo;
		using (FileStream stream = new FileStream (dataPath, FileMode.Open)) {
			playInfo = (SongPlayInfo)serializer.Deserialize (stream);
		}
		return playInfo;
	}
}
