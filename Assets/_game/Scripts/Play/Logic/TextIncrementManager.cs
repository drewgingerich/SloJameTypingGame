using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextIncrementManager {

	public TextIncrementManager (BeatActivityMonitor activityMonitor, ScoringChecker scoringChecker,
		TextManager textManager)
	{
		activityMonitor.OnMissedBeat += (_) => textManager.IncrementText ();
		scoringChecker.OnScoreBeat += textManager.IncrementText;
	}
}
