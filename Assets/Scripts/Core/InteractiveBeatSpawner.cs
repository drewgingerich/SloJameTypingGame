using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBeatSpawner : MonoBehaviour {

	// Connect in Inspector.
	[SerializeField] GameEventManager gameEventManager;
	[SerializeField] UpcomingBeatReader upcomingBeatReader;
	[SerializeField] RhythmSettings rhythmSettings;

	public event System.Action<InteractiveBeat> OnCreateInteractiveBeat;

	void Start () {
		upcomingBeatReader.OnUpcomingBeat += CreateInteractiveBeat;
	}

	void CreateInteractiveBeat (float timingOvershoot) {
		InteractiveBeat newActiveBeat = new InteractiveBeat ();
		newActiveBeat.Initialize (gameEventManager, rhythmSettings, timingOvershoot);
		if (OnCreateInteractiveBeat != null) {
			OnCreateInteractiveBeat (newActiveBeat);
		}
	}
}
