using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMapReader {

	BeatMap beatMap;
	int mapIndex;
	float spawnTime;
	float readAheadTime = 3f;
	bool mapFinished;

	public event System.Action<float, float> OnReadBeat;
	public event System.Action OnFinishMap;

	public BeatMapReader (BeatMap beatMap) {
		this.beatMap = beatMap;
		LookForNextBeat (0);
	}

	public void ReadMapUpToTime (float audioTime) {
		if (mapFinished)
			return;
		while (audioTime >= spawnTime && !mapFinished) {
			float targetTime = spawnTime + readAheadTime;
			if (OnReadBeat != null) 
				OnReadBeat (spawnTime, targetTime);
			LookForNextBeat (mapIndex + 1);
		}
	}

	void LookForNextBeat (int nextIndex) {
		if (nextIndex >= beatMap.BeatTimes.Count && OnFinishMap != null) {
			mapFinished = true;
			OnFinishMap ();
			return;
		}
		mapIndex = nextIndex;
		spawnTime = beatMap.BeatTimes [mapIndex];
	}
}
