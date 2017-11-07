using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLoopManager {

	BeatMapReader beatMapReader;
	BeatTimeManager beatTimeManager;
	BeatActivityMonitor beatActivityMonitor;
	ScoringChecker scoringChecker;

	public PlayLoopManager (BeatMapReader beatMapReader, BeatTimeManager beatTimeManager, 
		BeatActivityMonitor beatActivityMonitor, ScoringChecker scoringChecker) 
	{
		this.beatMapReader = beatMapReader;
		this.beatTimeManager = beatTimeManager;
		this.beatActivityMonitor = beatActivityMonitor;
		this.scoringChecker = scoringChecker;
	}

	public void PlayLoop (float audioTime, List<char> inputChars) {
		beatMapReader.ReadMapUpToTime (audioTime);
		beatTimeManager.UpdateBeatTimes (audioTime);
		List<Beat> activeBeats = beatActivityMonitor.ReportActiveBeats ();
		scoringChecker.CheckForScores(activeBeats, inputChars);
	}
}
