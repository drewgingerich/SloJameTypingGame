using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBeat {

	public event System.Action<InteractiveBeat> OnEnterActiveWindow;
	public event System.Action<InteractiveBeat> OnExitActiveWindow;
	public event System.Action<float> OnSuccess;
	public event System.Action OnFailure;
	public event System.Action OnLifetimeEnd;

	enum State { PreActive, Active, PostActive }

	State state;
	RhythmSettings rhythmSettings;
	float temporalDistance;


	public void Initialize (GameEventManager gameEventManager, RhythmSettings rhythmSettings, 
		float timingOvershoot) 
	{	
		state = State.PreActive;
		this.rhythmSettings = rhythmSettings;
		temporalDistance = rhythmSettings.indicatorTravelTime - timingOvershoot;
		gameEventManager.OnUpdateSongTime += UpdateTemporalDistance;
	}

	public void Success () {
		if (OnSuccess != null) {
			OnSuccess (temporalDistance);
		}
		if (OnLifetimeEnd != null) {
			OnLifetimeEnd ();
		}
	}

	public void Failure () {
		if (OnFailure != null) {
			OnFailure ();
		}
		if (OnLifetimeEnd != null) {
			OnLifetimeEnd ();
		}
	}

	void UpdateTemporalDistance (float deltaSongTime) {
		temporalDistance -= deltaSongTime;
		if (state == State.PreActive) {
			if (temporalDistance <= rhythmSettings.timingWindowHalfWidth) {
				state = State.Active;
				if (OnEnterActiveWindow != null) {
					OnEnterActiveWindow (this);
				}
			}
		} else if (state == State.Active) {
			if (temporalDistance <= rhythmSettings.timingWindowHalfWidth * -1) {
				state = State.PostActive;
				if (OnExitActiveWindow != null) {
					OnExitActiveWindow (this);
				}
				if (OnLifetimeEnd != null) {
					OnLifetimeEnd ();
				}
			}
		} 
	}
}
