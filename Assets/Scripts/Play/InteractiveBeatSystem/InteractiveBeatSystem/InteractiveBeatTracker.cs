using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBeatTracker {

	InteractiveBeatSpawner beatSpawner;
	float timingWindowHalfWidth;

	public List<InteractiveBeat> ActiveBeats { get; private set; }

	public event System.Action<InteractiveBeat> OnActivateBeat;
	public event System.Action<InteractiveBeat> OnDeactivateBeat;

	public InteractiveBeatTracker (InteractiveBeatSpawner beatSpawner, float timingWindowHalfWidth) {
		this.beatSpawner = beatSpawner;
		beatSpawner.OnSpawnBeat += RegisterBeat;
		this.timingWindowHalfWidth = timingWindowHalfWidth;
		ActiveBeats = new List<InteractiveBeat> ();
	}

	void RegisterBeat (InteractiveBeat interactiveBeat) {
		interactiveBeat.OnChangeTemporalDistance += CheckForActiveBeats;
	}

	public void UnregisterBeat (InteractiveBeat interactiveBeat) {
		interactiveBeat.OnChangeTemporalDistance -= CheckForActiveBeats;
		ActiveBeats.Remove (interactiveBeat);
	}
		
	void CheckForActiveBeats (InteractiveBeat interactiveBeat, float temporalDistanceFromBeat) {
		if (temporalDistanceFromBeat <= timingWindowHalfWidth * -1) {
			UnregisterBeat (interactiveBeat);
			if (OnDeactivateBeat != null)
				OnDeactivateBeat (interactiveBeat);
		} 	else if (temporalDistanceFromBeat <= timingWindowHalfWidth) {
			if (!ActiveBeats.Contains (interactiveBeat)) {
				ActiveBeats.Insert (0, interactiveBeat);
				if (OnActivateBeat != null)
					OnActivateBeat (interactiveBeat);
			}
		} 
	}
}
