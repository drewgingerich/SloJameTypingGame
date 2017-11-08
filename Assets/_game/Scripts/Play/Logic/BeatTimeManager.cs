using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTimeManager {

	List<Beat> beats;

	public BeatTimeManager (BeatSpawner spawner) {
		beats = new List<Beat> ();
		spawner.OnSpawnBeat += RegisterBeat;
	}

	void RegisterBeat (Beat beat) {
		beats.Add (beat);
		beat.OnDestroy += DeregisterBeat;
	}

	void DeregisterBeat (Beat beat) {
		beats.Remove (beat);
	}

	public void UpdateBeatTimes (float audioTime) {
		foreach (Beat beat in beats) {
			beat.UpdateProgress (audioTime);
		}
	}
}
