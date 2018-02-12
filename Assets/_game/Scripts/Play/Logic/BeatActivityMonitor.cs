using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeatActivityMonitor {

	float activeCountWindowHalfwidth = 0.5f;
	List<Beat> monitoredBeats;

	public event System.Action OnMissedBeat = delegate {};

	public BeatActivityMonitor (BeatSpawner spawner) {
		monitoredBeats = new List<Beat> ();
		spawner.OnSpawnBeat += RegisterBeat;
	}

	public List<Beat> ReportActiveBeats () {
		List<Beat> activeBeats = new List<Beat> ();
		List<Beat> missedBeats = new List<Beat> ();
		foreach (Beat beat in monitoredBeats) {
			if (beat.countsToTarget <= activeCountWindowHalfwidth * -1)
				missedBeats.Add (beat);
			else if (beat.countsToTarget <= activeCountWindowHalfwidth)
				activeBeats.Add (beat);
		}
		foreach (Beat beat in missedBeats) {
			DeregisterBeat (beat);
			OnMissedBeat ();
		}
		return activeBeats;
	}

	void RegisterBeat (Beat beat) {
		monitoredBeats.Add (beat);
		beat.OnDestroy += DeregisterBeat;
	}

	void DeregisterBeat (Beat beat) {
		monitoredBeats.Remove (beat);
	}
}
