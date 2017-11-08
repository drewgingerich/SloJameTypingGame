using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorBehavior : MonoBehaviour {

	Vector3 travelVector;
	Vector3 spawnPosition;

	public void Wire (Beat beat, Vector3 spawnPosition, Vector3 targetPosition) {
		beat.OnUpdateProgressRatio += UpdateBeatUI;
		this.spawnPosition = spawnPosition;
		travelVector = spawnPosition - targetPosition;
	}

	void UpdateBeatUI (Beat _, float progressRatio) {
		gameObject.transform.position = spawnPosition - travelVector * progressRatio;
	}
}
