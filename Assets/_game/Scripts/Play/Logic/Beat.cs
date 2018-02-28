using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat {

	public event System.Action<Beat, float> OnUpdateTimeToTarget = delegate { };
	public event System.Action<Beat, float> OnUpdateProgressRatio = delegate { };
	public event System.Action<Beat> OnDestroy = delegate { };

	public float countsToTarget;
	float targetCounts;
	float travelCounts;
	public float progressRatio;

	public char targetChar;

	public Beat(float countAtSpawn, float targetCount, char targetChar) {
		this.targetChar = targetChar;
		this.targetCounts = targetCount;
		this.travelCounts = targetCount - countAtSpawn;
	}

	public void UpdateProgress(float currentCounts) {
		countsToTarget = targetCounts - currentCounts;
		progressRatio = 1 - (countsToTarget / travelCounts);
		OnUpdateTimeToTarget(this, countsToTarget);
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