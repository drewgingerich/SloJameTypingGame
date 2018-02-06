using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMap  {

	public event System.Action OnFinishBeats = delegate {};

	List<float> beatTimes; 

	int beatIndex = 0;

	public BeatMap (List<float> beatTimes) {
		this.beatTimes = beatTimes;
		beatIndex = 0;
	}

	public BeatMap (SongData songInfo, int beatMapIndex) {
		BeatmapBlueprint blueprint = songInfo.blueprints[beatMapIndex];
		beatTimes = new List<float> ();
		float quarterNoteTime = 60 / songInfo.bpm;
		float indexTime = quarterNoteTime / 48;
		int beatIndex = 0;
		foreach (bool[] measureMap in blueprint.measures)
			foreach (bool flag in measureMap) {
				if (flag)
					beatTimes.Add (beatIndex * indexTime);
				beatIndex++;
			}
	}

	public float GetNextBeatTime () {
		float beatTime = beatTimes[beatIndex];
		beatIndex++;
		if (beatIndex >= beatTimes.Count)
			OnFinishBeats ();
		return beatTime;
	}
}
