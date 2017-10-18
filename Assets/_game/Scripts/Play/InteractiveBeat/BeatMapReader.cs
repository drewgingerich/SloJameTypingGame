using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMapReader {

	SongTimeBeatMap beatTimings;
	float timeSinceLastBeat;
	int beatIndex;
	float readAheadTime;

	public event System.Action<float, float> OnScoutBeat;

	public BeatMapReader (float readAheadTime, SongTimeBeatMap beatTimings, 
		AudioSectionPlayerBehavior playheadTracker) 
	{
		this.readAheadTime = readAheadTime;
		timeSinceLastBeat = readAheadTime * -1;
		beatIndex = 0;
		this.beatTimings = beatTimings;
		playheadTracker.OnChangePosition += ReadUpcomingBeats;
	}

	void ReadUpcomingBeats (float deltaSongTime) {
		timeSinceLastBeat += deltaSongTime;
		float beatDuration = 0.606f;
		float nextUpcomingBeatOffset = beatTimings.Timings [beatIndex] * beatDuration;

		if (timeSinceLastBeat >= nextUpcomingBeatOffset) {
			timeSinceLastBeat -= beatTimings.Timings [beatIndex] * beatDuration;
			// For now, loop through beat map to make testing easier.
			beatIndex = (beatIndex + 1) % beatTimings.Timings.Count;
			float timingOvershoot = timeSinceLastBeat;
			if (OnScoutBeat != null)
				OnScoutBeat (timingOvershoot, readAheadTime);
		}
	}
}
