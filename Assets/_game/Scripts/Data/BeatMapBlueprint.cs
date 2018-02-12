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

	public List<float> GetTargetCounts () {
		List<float> targetCounts = new List<float>();
		float intervalBeatValue = 4 / (float)BeatmapBlueprint.measureDivisor;
		for (int i = 0; i < measures.Count; i++) {
			for (int j = 0; j < BeatmapBlueprint.measureDivisor; j++) {
				if (measures[i][j]) {
					float targetCount = ((float)i * 4) + ((float)j * intervalBeatValue);
					targetCounts.Add(targetCount);
				}
			}
		}
		return targetCounts;
	}
}
