using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager {

	List<Beat> beats;

	public BeatManager (BeatSpawner spawner) {
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

	public void UpdateBeats (float beatCounts) {
		foreach (Beat beat in beats) {
			beat.UpdateProgress (beatCounts);
		}
	}
}
