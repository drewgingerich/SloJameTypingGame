using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat {

	public event System.Action<Beat, float> OnUpdateTimeToTarget = delegate { };
	public event System.Action<Beat, float> OnUpdateProgressRatio = delegate { };
	public event System.Action<Beat> OnDestroy = delegate { };

	public float timeToTarget;
	float targetTime;
	float travelTime;
	public float progressRatio;

	public char targetChar;

	public Beat(float spawnTime, float targetTime, char targetChar) {
		this.targetChar = targetChar;
		this.targetTime = targetTime;
		this.travelTime = targetTime - spawnTime;
	}

	public void UpdateProgress(float audioTime) {
		timeToTarget = targetTime - audioTime;
		progressRatio = 1 - (timeToTarget / travelTime);
		OnUpdateTimeToTarget(this, timeToTarget);
		OnUpdateProgressRatio(this, progressRatio);
	}

	public bool CheckForScore(ref List<char> inputChars) {
		if (inputChars.Contains(targetChar)) {
			inputChars.Remove(targetChar);
			return true;
		} else {
			return false;
		}
	}

	public void Destroy() {
		OnDestroy(this);
		OnUpdateTimeToTarget = null;
		OnUpdateProgressRatio = null;
		OnDestroy = null;
	}
}