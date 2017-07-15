using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBeat {

	public event System.Action<InteractiveBeat, float> OnChangeTemporalDistance;
	public event System.Action OnDestroy;

	public float TemporalDistanceFromBeat { get; private set; }

	public void Initialize (GameFlowManager gameEventManager, float timingOvershoot) {	
		gameEventManager.OnUpdateSongTime += UpdateTemporalDistance;
	}

	public void Destroy () {
		Debug.Log (TemporalDistanceFromBeat.ToString ());
		if (OnDestroy != null)
			OnDestroy ();
		OnChangeTemporalDistance = null;
		OnDestroy = null;
	}

	void UpdateTemporalDistance (float deltaSongTime) {
		TemporalDistanceFromBeat -= deltaSongTime;
		if (OnChangeTemporalDistance != null)
			OnChangeTemporalDistance (this, TemporalDistanceFromBeat);
	}
}
