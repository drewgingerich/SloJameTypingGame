using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayheadTracker {

	public event System.Action<float> OnReadPosition;
	public event System.Action<float> OnChangePosition;

	float lastReportedPosition;
	float position;
	float change;

	public void InitializeTracking (float startingPosition) {
		lastReportedPosition = startingPosition;
		position = startingPosition;
	}

	public void TrackByReportedPosition (float reportedPosition, float timeChange) {
		if (reportedPosition == lastReportedPosition) {
			TrackByTimeChange (timeChange);
		} else {
			change = reportedPosition - position;
			position = reportedPosition;
			lastReportedPosition = reportedPosition;
		}
		ReportPosition ();
	}

	public void TrackByTimeChange (float timeChange) {
		position += timeChange;
		change = timeChange;
		ReportPosition ();
	}

	void ReportPosition () {
		if (OnReadPosition != null) {
			OnReadPosition (position);
		}
		if (OnChangePosition != null)
			OnChangePosition (change);
	}
}
