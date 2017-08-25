using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBeat {

	public float FlightTime { get; private set; }
	public float TemporalDistance { get; private set; }

	public event System.Action<InteractiveBeat, float> OnChangeTemporalDistance;
	public event System.Action<float> OnChangeTripCompletionRatio; 

	public InteractiveBeat (float flightTime, float startTimeOffset, PlayheadTracker playheadTracker) {
		FlightTime = flightTime;
		TemporalDistance = flightTime - startTimeOffset;
		playheadTracker.OnReadDeltaPlayheadPosition += UpdateTemporalDistance;
	}

	void UpdateTemporalDistance (float deltaSongTime) {
		TemporalDistance -= deltaSongTime;
		if (OnChangeTemporalDistance != null)
			OnChangeTemporalDistance (this, TemporalDistance);
		if (OnChangeTripCompletionRatio != null)
			OnChangeTripCompletionRatio (TemporalDistance / FlightTime);
	}
}
