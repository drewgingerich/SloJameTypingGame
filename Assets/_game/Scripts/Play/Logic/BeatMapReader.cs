using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMapReader {

	BeatMap beatMap;
	TextKeeper textKeeper;
	float beatTime;
	float readAheadTime = 3f;
	bool mapFinished = false;

	public event System.Action<float, float, char> OnReadBeat = delegate {};
	public event System.Action OnFinishMap = delegate {};

	public BeatMapReader (BeatMap beatMap, TextKeeper textKeeper) {
		this.beatMap = beatMap;
		beatMap.OnFinishBeats += FinishSpawning;
		this.textKeeper = textKeeper;
		textKeeper.OnFinishText += FinishSpawning;
		beatTime = beatMap.GetNextBeatTime();
	}

	public void ReadMapUpToTime (float audioTime) {
		if (mapFinished)
			return;
		float seekTime = audioTime + readAheadTime;
		while (seekTime >= beatTime && !mapFinished) {
			OnReadBeat (beatTime - readAheadTime, beatTime, textKeeper.GetNextChar ());
			beatTime = beatMap.GetNextBeatTime ();
		}
	}

	void FinishSpawning () {
		mapFinished = true;
	}
}
