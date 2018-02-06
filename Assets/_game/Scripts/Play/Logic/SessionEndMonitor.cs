using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionEndMonitor {

	public event System.Action OnEndSession = delegate {};

	bool lastBeatSpawned = false;
	Beat mostRecentBeat;

	public SessionEndMonitor (AudioSectionPlayerBehavior audioSectionPlayer, BeatMapReader mapReader, 
		BeatSpawner spawner, TextKeeper textKeeper) 
	{
		audioSectionPlayer.OnEndSection += OnEndSession;
		textKeeper.OnFinishText += OnEndSession;
		spawner.OnSpawnBeat += RegisterBeat;
		mapReader.OnFinishMap += () => lastBeatSpawned = true;
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
