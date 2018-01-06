using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeatActivityMonitor {

	enum BeatState {Advancing, Active}

	float activeTimeWindowHalfwidth;
	List<Beat> monitoredBeats;

	public event System.Action<Beat> OnMissedBeat = delegate {};

	public BeatActivityMonitor (BeatSpawner spawner) {
		monitoredBeats = new List<Beat> ();
		spawner.OnSpawnBeat += RegisterBeat;
		activeTimeWindowHalfwidth = 0.2f;
	}

	public List<Beat> ReportActiveBeats () {
		List<Beat> activeBeats = new List<Beat> ();
		List<Beat> missedBeats = new List<Beat> ();
		foreach (Beat beat in monitoredBeats) {
			if (beat.timeToTarget <= activeTimeWindowHalfwidth * -1)
				missedBeats.Add (beat);
			else if (beat.timeToTarget <= activeTimeWindowHalfwidth)
				activeBeats.Add (beat);
		}
		foreach (Beat beat in missedBeats) {
			beat.Destroy ();
			// DeregisterBeat (beat);
			OnMissedBeat (beat);
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
