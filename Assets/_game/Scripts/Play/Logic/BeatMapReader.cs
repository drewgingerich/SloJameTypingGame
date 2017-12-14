using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMapReader {

	BeatMap beatMap;
	int mapIndex;
	float beatTime;
	float readAheadTime = 3f;
	bool mapFinished;

	public event System.Action<float, float> OnReadBeat = delegate {};
	public event System.Action OnFinishMap = delegate {};

	public BeatMapReader (BeatMap beatMap) {
		this.beatMap = beatMap;
		LookForNextBeat (0);
	}

	public void ReadMapUpToTime (float audioTime) {
		if (mapFinished)
			return;
		float seekTime = audioTime + readAheadTime;
		while (seekTime >= beatTime && !mapFinished) {
			OnReadBeat (beatTime - readAheadTime, beatTime);
			LookForNextBeat (mapIndex + 1);
		}
	}

	void LookForNextBeat (int nextIndex) {
		if (nextIndex >= beatMap.BeatTimes.Count) {
			mapFinished = true;
			OnFinishMap ();
			return;
		}
		mapIndex = nextIndex;
		beatTime = beatMap.BeatTimes [mapIndex];
	}
}
