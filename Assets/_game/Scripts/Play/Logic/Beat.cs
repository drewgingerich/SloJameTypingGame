using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat {

	public event System.Action<Beat, float> OnUpdateTimeToTarget = delegate {};
	public event System.Action<Beat, float> OnUpdateProgressRatio = delegate {};
	public event System.Action<Beat> OnDestroy = delegate {};

	public float timeToTarget { get; private set; }
	public int textIndex { get; private set; }

	float targetTime;
	float travelTime;
	char targetChar;

	public Beat (float spawnTime, float targetTime, int textIndex) {
		this.textIndex = textIndex;
		this.targetTime = targetTime;
		this.travelTime = targetTime - spawnTime;
	}

	public void UpdateProgress (float audioTime) {
		timeToTarget = targetTime - audioTime;
		OnUpdateTimeToTarget (this, timeToTarget);
		float progressRatio = 1 - (timeToTarget / travelTime);
		OnUpdateProgressRatio (this, progressRatio);
	}

	public void Destroy () {
		OnDestroy (this);
		OnUpdateTimeToTarget = null;
		OnUpdateProgressRatio = null;
		OnDestroy = null;
	}
}