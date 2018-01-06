using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMapReader {

	int textLength;
	int textIndex = 0;
	BeatMap beatMap;
	int mapIndex;
	float beatTime;
	float readAheadTime = 3f;
	bool mapFinished;

	public event System.Action<float, float, int> OnReadBeat = delegate {};
	public event System.Action OnFinishMap = delegate {};

	public BeatMapReader (BeatMap beatMap, int textLength) {
		this.beatMap = beatMap;
		this.textLength = textLength;
		LookForNextBeat (0);
	}

	public void ReadMapUpToTime (float audioTime) {
		if (mapFinished)
			return;
		float seekTime = audioTime + readAheadTime;
		while (seekTime >= beatTime && !mapFinished) {
			OnReadBeat (beatTime - readAheadTime, beatTime, textIndex);
			LookForNextBeat (mapIndex + 1);
		}
	}

	void LookForNextBeat (int nextIndex) {
		textIndex++;
		if (nextIndex >= beatMap.BeatTimes.Count || textIndex >= textLength) {
			mapFinished = true;
			OnFinishMap ();
			return;
		}
		mapIndex = nextIndex;
		beatTime = beatMap.BeatTimes [mapIndex];
	}
}
