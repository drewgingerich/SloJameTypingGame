// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayheadTracker {

// 	public event System.Action<float> OnUpdatePosition;

// 	float lastReportedPosition;
// 	float position;

// 	public void InitializeTracking (float startingPosition) {
// 		lastReportedPosition = startingPosition;
// 		position = startingPosition;
// 	}

// 	public void TrackByReportedPosition (float reportedPosition, float timeChange) {
// 		if (reportedPosition == lastReportedPosition) {
// 			TrackByTimeChange (timeChange);
// 		} else {
// 			position = reportedPosition;
// 			lastReportedPosition = reportedPosition;
// 		}
// 		UpdatePosition ();
// 	}

// 	public void TrackByTimeChange (float timeChange) {
// 		position += timeChange;
// 		UpdatePosition ();
// 	}

// 	void UpdatePosition () {
// 		if (OnUpdatePosition != null) {
// 			OnUpdatePosition (position);
// 		}
// 	}
// }
