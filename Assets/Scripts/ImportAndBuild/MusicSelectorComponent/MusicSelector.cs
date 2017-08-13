using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MusicSelector {

	readonly string musicDirectory = Application.persistentDataPath + "/Music/";

	public string[] GetMusicFilenames () {
		return Directory.GetFiles (musicDirectory);
	}
}
