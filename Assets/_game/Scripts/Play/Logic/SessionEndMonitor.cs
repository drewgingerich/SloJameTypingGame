using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionEndMonitor {

	public event System.Action OnEndSession;

	bool lastBeatSpawned = false;
	Beat mostRecentBeat;

	public SessionEndMonitor (AudioSectionPlayerBehavior audioSectionPlayer, BeatMapReader mapReader, 
		BeatSpawner spawner, TextManager textManager) 
	{
		audioSectionPlayer.OnEndSection += EndSession;
		textManager.OnEndText += EndSession;
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
			EndSession ();
	}

	void EndSession () {
		if (OnEndSession != null)
			OnEndSession ();
	}
}
