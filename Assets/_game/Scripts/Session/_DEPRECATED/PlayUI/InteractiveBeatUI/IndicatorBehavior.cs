// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class IndicatorBehavior : MonoBehaviour {

// 	Vector3 targetPosition;
// 	Beat trackedBeat;
// 	Vector3 travelVector;

// 	public void setTrajectory (Vector3 spawnPosition, Vector3 targetPosition) {
// 		this.targetPosition = targetPosition;
// 		travelVector = spawnPosition - targetPosition;
// 	}

// 	public void setTrackedBeat (Beat beat) {
// 		trackedBeat = beat;
// 		trackedBeat.OnChangeTripCompletionRatio += UpdateIndicatorPosition;
// 	}
		
// 	void UpdateIndicatorPosition (float tripCompletionRatio) {
// 		Vector3 newPosition = targetPosition + travelVector * tripCompletionRatio;
// 		transform.position = newPosition;
// 	}
// }
