using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMapReader {

	int textLength;
	BeatMap beatMap;
	int mapIndex = 0;
	float beatTime;
	float readAheadTime = 3f;
	bool mapFinished;

	public event System.Action<float, float, int> OnReadBeat = delegate {};
	public event System.Action OnFinishMap = delegate {};

	public BeatMapReader (BeatMap beatMap, int textLength) {
		this.beatMap = beatMap;
		this.textLength = textLength;
		LookForNextBeat ();
	}

	public void ReadMapUpToTime (float audioTime) {
		if (mapFinished)
			return;
		float seekTime = audioTime + readAheadTime;
		while (seekTime >= beatTime && !mapFinished) {
			OnReadBeat (beatTime - readAheadTime, beatTime, mapIndex);
			mapIndex++;
			LookForNextBeat ();
		}
	}

	void LookForNextBeat () {
		if (mapIndex >= beatMap.BeatTimes.Count || mapIndex >= textLength) {
			mapFinished = true;
			OnFinishMap ();
			return;
		}
		beatTime = beatMap.BeatTimes [mapIndex];
	}
}
