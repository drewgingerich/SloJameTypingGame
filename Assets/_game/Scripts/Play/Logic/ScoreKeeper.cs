using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper {

	public event System.Action<int> OnUpdateScore;

	int beatsScored = 0;
	int score = 0;

	public ScoreKeeper (BeatActivityMonitor beatActivityMonitor, ScoringChecker scoringChecker) {
		beatActivityMonitor.OnMissedBeat += (_) => ScoreMiss ();
		scoringChecker.OnScoreBeat += ScoreSuccess;
	}

	public float GetScorePercentage () {
		float scoreRatio = score / beatsScored;
		float scorePercentage = scoreRatio * 100;
		return scorePercentage;
	}

	void ScoreSuccess () {
		beatsScored++;
		score++;
		if (OnUpdateScore != null)
			OnUpdateScore (score);
	}

	void ScoreMiss () {
		beatsScored++;
		if (OnUpdateScore != null)
			OnUpdateScore (score);
	}
}
