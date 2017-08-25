using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SongClipReader {

	public static AudioClip RetrieveClip (string path) {
		WWW www = new WWW (path);
		AudioClip clip = WWWAudioExtensions.GetAudioClip (www, false, false);
		while (clip.loadState != AudioDataLoadState.Loaded)
			continue;
		return clip;
	}
}
