using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMap  {

	public List<float> BeatTimes { get; private set; }

	public BeatMap (List<float> beatTimes) {
		BeatTimes = beatTimes;
	}

	public BeatMap (SongInfo songInfo, int beatMapIndex) {
		float quarterNoteTime = 60 / songInfo.bpm;
		float indexTime = quarterNoteTime / 48;
		int indexSinceLastBeat = 0;
		foreach (bool[] measureMap in songInfo.blueprints [beatMapIndex].measures)
			foreach (bool flag in measureMap) {
				if (flag) {
					BeatTimes.Add (indexSinceLastBeat * indexTime);
					indexSinceLastBeat = 0;
				} else {
					indexSinceLastBeat++;
				}
			}
	}
}
