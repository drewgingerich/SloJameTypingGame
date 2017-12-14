using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMap  {

	public List<float> BeatTimes { get; private set; }

	public BeatMap (List<float> beatTimes) {
		BeatTimes = beatTimes;
	}

	public BeatMap (SongData songInfo, int beatMapIndex) {
		BeatmapBlueprint blueprint = songInfo.blueprints[beatMapIndex];
		BeatTimes = new List<float> ();
		float quarterNoteTime = 60 / songInfo.bpm;
		float indexTime = quarterNoteTime / 48;
		int beatIndex = 0;
		foreach (bool[] measureMap in blueprint.measures)
			foreach (bool flag in measureMap) {
				if (flag)
					BeatTimes.Add (beatIndex * indexTime);
				beatIndex++;
			}
	}
}
