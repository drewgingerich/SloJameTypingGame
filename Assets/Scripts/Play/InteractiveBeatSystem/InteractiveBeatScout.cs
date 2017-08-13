using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBeatScout {

	InteractiveBeatMap beatMap;
	float timeSinceLastBeat;
	int beatIndex;
	float readAheadTime;

	public event System.Action<float, float> OnScoutBeat;

	public InteractiveBeatScout (float readAheadTime, InteractiveBeatMap beatMap, 
		PlayheadTracker playheadTracker) 
	{
		this.readAheadTime = readAheadTime;
		timeSinceLastBeat = readAheadTime * -1;
		beatIndex = 0;
		this.beatMap = beatMap;
		playheadTracker.OnReadDeltaPlayheadPosition += ScoutUpcomingBeats;
	}

	public void ScoutUpcomingBeats (float deltaSongTime) {
		timeSinceLastBeat += deltaSongTime;
		float beatDuration = 0.606f;
		float nextUpcomingBeatOffset = beatMap.beatOffsets [beatIndex] * beatDuration;

		if (timeSinceLastBeat >= nextUpcomingBeatOffset) {
			timeSinceLastBeat -= beatMap.beatOffsets [beatIndex] * beatDuration;
			// For now, loop through beat map to make testing easier.
			beatIndex = (beatIndex + 1) % beatMap.beatOffsets.Count;
			float timingOvershoot = timeSinceLastBeat;
			if (OnScoutBeat != null)
				OnScoutBeat (timingOvershoot, readAheadTime);
		}
	}
}
