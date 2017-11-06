using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper {

	public event System.Action<int> OnUpdateScore;

	int score = 0;

	public ScoreKeeper (BeatActivityMonitor beatActivityMonitor, ScoringChecker scoringChecker) {
		beatActivityMonitor.OnMissedBeat += (_) => ScoreMiss ();
		scoringChecker.OnScoreBeat += ScoreSuccess;
	}

	void ScoreSuccess () {
		score++;
		if (OnUpdateScore != null)
			OnUpdateScore (score);
	}

	void ScoreMiss () {
		score--;
		if (OnUpdateScore != null)
			OnUpdateScore (score);
	}
}
