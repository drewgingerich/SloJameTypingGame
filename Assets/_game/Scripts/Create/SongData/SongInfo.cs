using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SongInfo {

	public string songTitle;
	public float bpm;
	public float songOffset;
	public float songDuration;
	public List<BeatMapBlueprint> blueprints;

	public SongInfo () {
		songTitle = "default_title";
		bpm = 100f;
		songOffset = 0f;
		songDuration = 60f;
		blueprints = new List<BeatMapBlueprint> ();
	}
}
