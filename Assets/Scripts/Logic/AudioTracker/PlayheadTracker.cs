using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayheadTracker {

	float lastReportedPlayheadPosition;

	public float PlayheadPosition { get; private set; }
	public float DeltaPlayheadPosition { get; private set; }

	public event System.Action<float> OnReadPlayheadPosition;
	public event System.Action<float> OnReadDeltaPlayheadPosition;

	public PlayheadTracker (float playheadStartingPosition) {
		lastReportedPlayheadPosition = playheadStartingPosition;
		PlayheadPosition = playheadStartingPosition;
	}
		
	public void TrackPlayheadPosition (float reportedPlayheadPosition, float timeChange) {
		if (lastReportedPlayheadPosition != reportedPlayheadPosition) {
			DeltaPlayheadPosition = reportedPlayheadPosition - PlayheadPosition;
			lastReportedPlayheadPosition = reportedPlayheadPosition;
		} else {
			DeltaPlayheadPosition = timeChange;
		}
		PlayheadPosition += DeltaPlayheadPosition;
		if (OnReadPlayheadPosition != null)
			OnReadPlayheadPosition (PlayheadPosition);
		if (OnReadDeltaPlayheadPosition != null)
			OnReadDeltaPlayheadPosition (DeltaPlayheadPosition);
	}
}
