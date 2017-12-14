using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockSessionStarter : MonoBehaviour {

	[SerializeField] PlayManagerBehavior managerBehavior;
	[SerializeField] [TextArea] string text;

	// Use this for initialization
	IEnumerator Start () {
		SongData song = DataNavigator.currentSong;
		WWW www = new WWW ("file://" + song.directoryPath + "/" + song.songTitle + ".wav");
		yield return www;
		AudioClip clip = www.GetAudioClip ();
		BeatMap map = new BeatMap (DataNavigator.currentSong, DataNavigator.beatmapIndex);
		managerBehavior.StartPlay (clip, map, text);
	}
}
