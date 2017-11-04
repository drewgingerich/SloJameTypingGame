using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLoop {

	BeatMapReader beatMapReader;
	BeatTimeManager beatTimeManager;
	BeatActivityMonitor beatActivityMonitor;
	ScoringChecker scoreManager;

	public PlayLoop (BeatMapReader beatMapReader, BeatTimeManager beatTimeManager, BeatActivityMonitor beatActivityMonitor) {
		this.beatMapReader = beatMapReader;
		this.beatTimeManager = beatTimeManager;
		this.beatActivityMonitor = beatActivityMonitor;
	}

	public void Loop (float audioTime, List<char> inputChars) {
		beatMapReader.ReadMapUpToTime (audioTime);
		beatTimeManager.UpdateBeatTimes (audioTime);
		List<Beat> activeBeats = beatActivityMonitor.ReportActiveBeats ();
		scoreManager.CheckForScores(activeBeats, inputChars);
	}
}
