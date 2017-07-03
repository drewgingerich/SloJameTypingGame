using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatIndicator : MonoBehaviour {

	//SongManager songManager;

	Vector3 targetPosition;
	Vector3 travelVector;
	float timeToTarget;
	float flightTime;

	public void SetSongManager (SongManager songManager) {
		//this.songManager = songManager;
		songManager.OnReadPlayheadPosition += UpdatePosition;
	}

	public void setTrajectory(Vector3 spawnPosition, Vector3 targetPosition, float timeToTarget) {
		this.targetPosition = targetPosition;
		this.timeToTarget = timeToTarget;
		travelVector = spawnPosition - targetPosition;
		flightTime = 0;
	}
		
	void UpdatePosition (float deltaSongTime) {
		flightTime += deltaSongTime;
		float tripCompletionRatio = flightTime / timeToTarget;
		Vector3 newPosition = targetPosition + travelVector * (1 - tripCompletionRatio);
		gameObject.transform.position = newPosition;
	}
		
	void OnTriggerExit2D (Collider2D other) {
		gameObject.SetActive (false);
	}
}
