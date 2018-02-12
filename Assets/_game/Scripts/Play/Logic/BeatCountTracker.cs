using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCountTracker {

	float tempo;

	public BeatCountTracker (SongData songData) {
		tempo = songData.bpm / 60;
	}

	public float GetBeatCountsAtTime (float time) {
		return time * tempo;
	}
}
