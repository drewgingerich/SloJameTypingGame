using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionEndMonitor {

	public event System.Action OnEndSession;

	bool lastBeatSpawned;
	Beat mostRecentBeat;

	public SessionEndMonitor (AudioSectionPlayerBehavior audioSectionPlayer, BeatMapReader mapReader, 
		BeatSpawner spawner, TextManager textManager) 
	{
		audioSectionPlayer.OnEndSection += EndSession;
		textManager.OnTextEnd += EndSession;
		spawner.OnSpawnBeat += RegisterBeat;
		mapReader.OnFinishMap += () => lastBeatSpawned = true;
	}

	void RegisterBeat (Beat beat) {
		if (mostRecentBeat != null)
			DeregisterBeat (mostRecentBeat);
		beat.OnDestroy += EndSessionDueToLastBeat;
	}

	void DeregisterBeat (Beat beat) {
		beat.OnDestroy -= EndSessionDueToLastBeat;
	}

	void EndSessionDueToLastBeat (Beat _) {
		EndSession ();
	}

	void EndSession () {
		if (OnEndSession != null)
			OnEndSession ();
	}
}
