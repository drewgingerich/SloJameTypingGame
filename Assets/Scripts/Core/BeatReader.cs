using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatReader : MonoBehaviour {

	[SerializeField] SongManager songManager;
	[SerializeField] SongBeats songBeats;
	[SerializeField] RhythmSettings rhythmSettings;

	public event System.Action OnBeat;
	public event System.Action OnNoBeat;
	public event System.Action OnBeatStart;
	public event System.Action OnBeatEnd;

	enum BeatState { NoBeat, Beat };

	BeatState beatState;
	float timeSinceLastBeat;
	int beatIndex;

	void Awake () {
		beatState = BeatState.NoBeat;
		beatIndex = 0;
	}

	void Start () {
		songManager.OnReadPlayheadPosition += CheckBeatState;
		timeSinceLastBeat = rhythmSettings.indicatorTravelTime * -1;
	}

	void CheckBeatState (float deltaSongTime) {
		timeSinceLastBeat += deltaSongTime;
		float nextBeatOffset = songBeats.beatOffsets [beatIndex] * rhythmSettings.beatDuration;

		float lowerBeatActivityThreshold = nextBeatOffset - rhythmSettings.timingWindowHalfWidth;
		float upperBeatActivityThreshold = nextBeatOffset + rhythmSettings.timingWindowHalfWidth;

		if (beatState == BeatState.Beat) {
			if (timeSinceLastBeat >= upperBeatActivityThreshold) {
				beatState = BeatState.NoBeat;
				IncrementBeatIndex ();
				OnBeatEnd ();
			} else {
				OnBeat ();
			}
		} else if (beatState == BeatState.NoBeat) {
			if (timeSinceLastBeat >= lowerBeatActivityThreshold) {
				beatState = BeatState.Beat;
				if (OnBeatStart != null) {
					OnBeatStart ();
				}
				OnBeat ();
			} else {
				OnNoBeat ();
			}
		}
	}

	void IncrementBeatIndex () {
		// For now, loop through beat map for testing purposes.
		beatIndex = (beatIndex + 1) % songBeats.beatOffsets.Count;
		timeSinceLastBeat -= songBeats.beatOffsets [beatIndex] * rhythmSettings.beatDuration;
	}
}
