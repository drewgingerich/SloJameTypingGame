// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ScoreManagerBehavior {

// 	BeatActivityMonitor beatTracker;
// 	TextManager passageReader;

// 	int score;
// 	int combo;
// 	//bool comboBroken;

// 	public enum ScoreCategory { Missed, Failed, Succeeded } 

// 	public event System.Action<int, int, ScoreCategory, Beat> OnScoreBeat;

// 	public ScoreManagerBehavior (BeatActivityMonitor beatTracker, TextManager passageReader) {
// 		this.beatTracker = beatTracker;
// 		beatTracker.OnDeactivateBeat += ScoreMissedBeat;
// 		this.passageReader = passageReader;
// 		score = 0;
// 		combo = 0;
// 	}

// 	public void ScoreActiveBeats (string inputString) {
// 		List<Beat> activeBeats = new List<Beat> (beatTracker.activeBeats);
// 		int numberOfBeatsToScore = Mathf.Min (inputString.Length, activeBeats.Count);
// 		for (int i = 0; i < numberOfBeatsToScore; i++) {
// 			char inputChar = inputString [i];
// 			if (inputChar == passageReader.GetUpcomingCharacter (0)) {
// 				ScoreSuccessfulBeat (activeBeats [i]);
// 			} else {
// 				ScoreFailedBeat (activeBeats [i]);
// 			}
// 		}
// 	}

// 	void ScoreBeat (Beat scoredBeat, ScoreCategory scoreCategory) {
// 		if (OnScoreBeat != null) {
// 			OnScoreBeat (score, combo, scoreCategory, scoredBeat);
// 		}
// 		beatTracker.UnregisterBeat (scoredBeat);
// 		passageReader.IncrementText (1);
// 	}

// 	void ScoreMissedBeat (Beat scoredBeat) {
// 		combo = 0;
// 		ScoreBeat (scoredBeat, ScoreCategory.Missed);
// 	}

// 	void ScoreSuccessfulBeat (Beat scoredBeat) {
// 		combo++;
// 		score += combo;
// 		ScoreBeat (scoredBeat, ScoreCategory.Succeeded);
// 	}

// 	void ScoreFailedBeat (Beat scoredBeat) {
// 		combo = 0;
// 		ScoreBeat (scoredBeat, ScoreCategory.Failed);
// 	}
// }
