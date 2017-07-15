using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBeatSpawner : MonoBehaviour {

	[SerializeField] GameFlowManager gameEventManager;
	[SerializeField] UpcomingInteractiveBeatReader upcomingBeatReader;
	[SerializeField] PlaySettings playSettings;

	public event System.Action<InteractiveBeat> OnSpawnInteractiveBeat;

	void Start () {
		upcomingBeatReader.OnUpcomingBeat += SpawnInteractiveBeat;
	}

	void SpawnInteractiveBeat (float timingOvershoot) {
		InteractiveBeat newActiveBeat = new InteractiveBeat ();
		newActiveBeat.Initialize (gameEventManager, timingOvershoot);
		if (OnSpawnInteractiveBeat != null)
			OnSpawnInteractiveBeat (newActiveBeat);
	}
}
