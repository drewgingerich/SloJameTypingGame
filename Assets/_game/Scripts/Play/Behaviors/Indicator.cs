using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour {

	Vector3 travelVector;
	Vector3 spawnPosition;

	public void Wire (Beat beat, Vector3 spawnPosition, Vector3 targetPosition) {
		beat.OnUpdateProgressRatio += UpdateBeatUI;
		beat.OnDestroy += (_) => Destroy ();
		this.spawnPosition = spawnPosition;
		travelVector = targetPosition - spawnPosition;
	}

	void UpdateBeatUI (Beat _, float progressRatio) {
		gameObject.transform.position = spawnPosition + travelVector * progressRatio;
	}

	void Destroy () {
		Destroy (gameObject);
	}
}
