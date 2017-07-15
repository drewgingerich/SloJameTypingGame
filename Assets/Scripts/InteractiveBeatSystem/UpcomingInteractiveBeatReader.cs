using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpcomingInteractiveBeatReader : MonoBehaviour {

	[SerializeField] GameFlowManager gameEventManager;
	[SerializeField] SongBeats songBeats;
	[SerializeField] PlaySettings rhythmSettings;

	public event System.Action<float> OnUpcomingBeat;

	int beatIndex;
	float timeSinceLastBeat;

	void Awake () {
		beatIndex = 0;
		timeSinceLastBeat = 0;
	}

	void Start () {
		gameEventManager.OnCheckForNewInteractiveBeats += CheckForUpcomingBeat;
	}

	void CheckForUpcomingBeat (float deltaSongTime) {
		timeSinceLastBeat += deltaSongTime;
		float nextUpcomingBeatOffset = songBeats.beatOffsets [beatIndex] * rhythmSettings.beatDuration;

		if (timeSinceLastBeat >= nextUpcomingBeatOffset) {
			if (OnUpcomingBeat != null) {
				timeSinceLastBeat -= songBeats.beatOffsets [beatIndex] * rhythmSettings.beatDuration;
				// For now, loop through beat map to make testing easier.
				beatIndex = (beatIndex + 1) % songBeats.beatOffsets.Count;
				float timingOvershoot = timeSinceLastBeat;
				OnUpcomingBeat (timingOvershoot);
			}
		}
	}
}
