using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureBeatReader : MonoBehaviour {

	[SerializeField] SongManager songManager;
	[SerializeField] SongBeats songBeats;
	[SerializeField] RhythmSettings rhythmSettings;

	public event System.Action OnFutureBeat;

	float timeSinceLastBeat;
	int beatIndex;

	void Awake () {
		beatIndex = 0;
		timeSinceLastBeat = 0;
	}

	void Start () {
		songManager.OnReadPlayheadPosition += CheckBeatState;
	}

	void CheckBeatState (float deltaSongTime) {
		timeSinceLastBeat += deltaSongTime;
		float nextFutureBeatOffset = songBeats.beatOffsets [beatIndex] * rhythmSettings.beatDuration;

		if (timeSinceLastBeat >= nextFutureBeatOffset) {
			if (OnFutureBeat != null) {
				IncrementBeatIndex ();
				OnFutureBeat ();
			}
		}
	}

	void IncrementBeatIndex () {
		// For now, loop through beat map for testing purposes.
		beatIndex = (beatIndex + 1) % songBeats.beatOffsets.Count;
		timeSinceLastBeat -= songBeats.beatOffsets [beatIndex] * rhythmSettings.beatDuration;
	}
}
