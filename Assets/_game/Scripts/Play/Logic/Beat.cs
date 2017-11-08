using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat {

	float targetTime;
	float travelTime;
	public float timeToTarget { get; private set; }

	public event System.Action<Beat, float> OnUpdateTimeToTarget;
	public event System.Action<Beat, float> OnUpdateProgressRatio;
	public event System.Action<Beat> OnDestroy;

	public Beat (float spawnTime, float targetTime) {
		this.targetTime = targetTime;
		travelTime = targetTime - spawnTime;
	}

	public void UpdateProgress (float audioTime) {
		timeToTarget = targetTime - audioTime;
		if (OnUpdateTimeToTarget != null)
			OnUpdateTimeToTarget (this, timeToTarget);
		float progressRatio = 1 - (timeToTarget / travelTime);
		if (OnUpdateProgressRatio != null)
			OnUpdateProgressRatio (this, progressRatio);
	}

	public void Destroy () {
		if (OnDestroy != null)
			OnDestroy (this);
		OnUpdateTimeToTarget = null;
		OnUpdateProgressRatio = null;
		OnDestroy = null;
	}
}