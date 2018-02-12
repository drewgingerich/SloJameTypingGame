using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLoopManager {

	BeatCountTracker countTracker;
	BeatSpawner spawner;
	BeatManager beatManager;
	BeatActivityMonitor activityMonitor;
	ScoringChecker scoringChecker;

	public PlayLoopManager (BeatCountTracker countTracker, BeatSpawner spawner, BeatManager beatManager, 
		BeatActivityMonitor activityMonitor, ScoringChecker scoringChecker) 
	{
		this.countTracker = countTracker;
		this.spawner = spawner;
		this.beatManager = beatManager;
		this.activityMonitor = activityMonitor;
		this.scoringChecker = scoringChecker;
	}

	public void PlayLoop (float audioTime, List<char> inputChars) {
		float counts = countTracker.GetBeatCountsAtTime (audioTime);
		spawner.CheckForBeatSpawns (counts);
		beatManager.UpdateBeats (counts);
		List<Beat> activeBeats = activityMonitor.ReportActiveBeats ();
		scoringChecker.CheckForScores(activeBeats, inputChars);
	}
}
