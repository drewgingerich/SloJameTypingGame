using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBeatSpawner {

	PlayheadTracker playheadTracker;

	public event System.Action<InteractiveBeat> OnSpawnBeat;

	public InteractiveBeatSpawner (InteractiveBeatScout beatScout, PlayheadTracker playheadTracker) {
		beatScout.OnScoutBeat += SpawnInteractiveBeat;
		this.playheadTracker = playheadTracker;
	}

	void SpawnInteractiveBeat (float timingOvershoot, float readAheadTime) {
		InteractiveBeat newBeat = new InteractiveBeat (readAheadTime, timingOvershoot, playheadTracker);
		if (OnSpawnBeat != null)
			OnSpawnBeat (newBeat);
	}
}
