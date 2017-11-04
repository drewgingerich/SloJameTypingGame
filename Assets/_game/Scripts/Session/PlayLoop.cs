using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLoop {

	BeatMapReader beatMapReader;
	BeatTimeManager beatTimeManager;
	BeatActivityMonitor beatActivityMonitor;
	ScoreManager scoreManager;

	public PlayLoop (BeatMapReader beatMapReader, BeatTimeManager beatTimeManager, BeatActivityMonitor beatActivityMonitor) {
		this.beatMapReader = beatMapReader;
		this.beatTimeManager = beatTimeManager;
		this.beatActivityMonitor = beatActivityMonitor;
	}

	public void Loop (float audioTime, List<char> inputChars) {
		beatMapReader.ReadMapUpToTime (audioTime);
		beatTimeManager.UpdateBeatTimes (audioTime);
		int numberOfActiveBeats = beatActivityMonitor.ReportNumberOfActiveBeats ();
		scoreManager.UpdateScore(numberOfActiveBeats, inputChars);
	}
}
