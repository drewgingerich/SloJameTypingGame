using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMapReader {

	BeatMap beatMap;
	int mapIndex;
	float spawnTime;
	float readAheadTime = 1.5f;

	public event System.Action<float, float> OnReadBeat;
	public event System.Action OnFinishMap;

	public BeatMapReader (BeatMap beatMap) {
		this.beatMap = beatMap;
		LookForNextBeat (0);
		}

	public void ReadMapUpToTime (float audioTime) {
		while (audioTime >= spawnTime) {
			float targetTime = spawnTime + readAheadTime;
			if (OnReadBeat != null) 
				OnReadBeat (spawnTime, targetTime);
			LookForNextBeat (mapIndex + 1);
		}
	}

	void LookForNextBeat (int nextIndex) {
		if (mapIndex >= beatMap.BeatTimes.Count && OnFinishMap != null) {
			OnFinishMap ();
			return;
		}
		mapIndex = nextIndex;
		spawnTime = beatMap.BeatTimes [mapIndex];
	}
}
