using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLoop {

	BeatMapReader beatMapReader;
	BeatTimeManager beatTimeManager;
	BeatActivityMonitor beatActivityMonitor;
	ScoringChecker scoringChecker;

	public PlayLoop (BeatMapReader beatMapReader, BeatTimeManager beatTimeManager, 
		BeatActivityMonitor beatActivityMonitor, ScoringChecker scoringChecker) 
	{
		this.beatMapReader = beatMapReader;
		this.beatTimeManager = beatTimeManager;
		this.beatActivityMonitor = beatActivityMonitor;
		this.scoringChecker = scoringChecker;
	}

	public void Loop (float audioTime, List<char> inputChars) {
		beatMapReader.ReadMapUpToTime (audioTime);
		beatTimeManager.UpdateBeatTimes (audioTime);
		List<Beat> activeBeats = beatActivityMonitor.ReportActiveBeats ();
		scoringChecker.CheckForScores(activeBeats, inputChars);
	}
}
