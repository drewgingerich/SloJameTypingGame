using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongTimeBeatMap  {

	public List<float> Timings { get; private set; }

	public SongTimeBeatMap (SongInfo songInfo, int beatMapIndex) {
		float quarterNoteTime = 60 / songInfo.bpm;
		float indexTime = quarterNoteTime / 48;
		int indexesSinceLastBeat = 0;
		foreach (bool[] measureMap in songInfo.blueprints [beatMapIndex].measures)
			foreach (bool flag in measureMap) {
				if (flag) {
					Timings.Add (indexesSinceLastBeat * indexTime);
					indexesSinceLastBeat = 0;
				} else {
					indexesSinceLastBeat++;
				}
			}
	}
}
