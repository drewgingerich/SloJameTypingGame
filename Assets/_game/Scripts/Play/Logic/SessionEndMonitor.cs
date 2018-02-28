using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionEndMonitor {

	public event System.Action OnEndSession = delegate {};

	bool lastBeatSpawned = false;
	Beat mostRecentBeat;

	public SessionEndMonitor (SmartAudioSource audioSource, BeatMapReader mapReader, 
		BeatSpawner spawner, TextReader textKeeper) 
	{
		audioSource.OnStop += OnEndSession;
		textKeeper.OnFinishText += () => lastBeatSpawned = true;
		mapReader.OnFinishBeatMap += () => lastBeatSpawned = true;
		spawner.OnSpawnBeat += RegisterBeat;
	}

	void RegisterBeat (Beat beat) {
		if (mostRecentBeat != null)
			DeregisterBeat (mostRecentBeat);
		mostRecentBeat = beat;
		mostRecentBeat.OnDestroy += CheckIfLastBeat;
	}

	void DeregisterBeat (Beat beat) {
		beat.OnDestroy -= CheckIfLastBeat;
	}

	void CheckIfLastBeat (Beat _) {
		if (lastBeatSpawned)
			OnEndSession ();
	}
}
