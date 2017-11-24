// using System.Collections;
// using System.Collections.Generic;
// using System.Xml;
// using System.Xml.Serialization;
// using System.IO;
// using UnityEngine;

// public class GroupData {

// 	public string directoryPath;
// 	public string groupTitle;

// 	public List<SongData> GetSongsInGroup () {
// 		List<SongData> songDatas = new List<SongData> ();
// 		foreach (string songDirectoryPath in Directory.GetDirectories (directoryPath)) {
// 			SongData songData = SongData.Load (songDirectoryPath);
// 			songDatas.Add (songData);
// 		}
// 		return songDatas;
// 	}

// 	public List<GroupData> GetGroupsInGroup () {
// 		List<GroupData> groupDatas = new List<GroupData> ();
// 		foreach (string songDirectoryPath in Directory.GetDirectories (directoryPath)) {
// 			GroupData groupData = GroupData.Load (directoryPath);
// 			groupDatas.Add (groupData);
// 		}
// 		return groupDatas;
// 	}

// 	public void Save () {
// 		string tempPath = directoryPath + "~" + groupTitle + ".xml~";
// 		string finalPath = directoryPath + groupTitle + ".xml";
// 		XmlSerializer serializer = new XmlSerializer (typeof (SongData));
// 		using (FileStream stream = new FileStream (tempPath, FileMode.Create))
// 			serializer.Serialize(stream, this);
// 		File.Copy (tempPath, finalPath, true);
// 		File.Delete (tempPath);
// 	}

// 	public static GroupData Load (string directoyPath) {
// 		XmlSerializer serializer = new XmlSerializer (typeof (SongData));
// 		string groupTitle = Path.GetDirectoryName (directoyPath);
// 		string dataPath = Path.Combine (directoyPath, groupTitle + ".xml");
// 		if (!File.Exists (dataPath))
// 			return new GroupData (directoyPath, groupTitle);
// 		using (FileStream stream = new FileStream (dataPath, FileMode.Open))
// 			return (GroupData)serializer.Deserialize (stream);
// 	}
// }
