using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper {

	public event System.Action<int> OnUpdateScore = delegate {};

	public int totalNumberBeats { get; private set; }
	public int beatsHit { get; private set; }

	public ScoreKeeper (BeatActivityMonitor beatActivityMonitor, ScoringChecker scoringChecker) {
		beatActivityMonitor.OnMissedBeat += (_) => ScoreMiss ();
		scoringChecker.OnScoreBeat += ScoreSuccess;
		totalNumberBeats = 0;
		beatsHit = 0;
	}

	public float GetScorePercentage () {
		float scoreRatio = (float)beatsHit / totalNumberBeats;
		float scorePercentage = scoreRatio * 100;
		return scorePercentage;
	}

	void ScoreSuccess () {
		totalNumberBeats++;
		beatsHit++;
		OnUpdateScore (beatsHit);
	}

	void ScoreMiss () {
		totalNumberBeats++;
		OnUpdateScore (beatsHit);
	}
}
