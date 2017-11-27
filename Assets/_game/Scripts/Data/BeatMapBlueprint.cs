using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatmapBlueprint {

	public const int measureDivisor = 192;

	public List<bool[]> measures;

	public BeatmapBlueprint () {
		measures = new List<bool[]> ();
	}

	public void AddMeasure () {
		measures.Add (new bool [measureDivisor]);
	}

	public List<float> Translate (SongData songData) {
		List<float> beatTimings = new List<float> ();
		float beatDistance = 60 / (48 * songData.bpm);
		float songTime = 0;
		foreach (bool[] measure in measures) {
			foreach (bool beat in measure) {
				if (beat)
					beatTimings.Add (songTime);
				songTime += beatDistance;
			}
		}
		return beatTimings;
	}
}
