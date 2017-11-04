using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringChecker {

	TextManager textManager;

	public event System.Action OnScoreBeat;

	public ScoringChecker (TextManager textManager) {
		this.textManager = textManager;
	}

	public void CheckForScores (List<Beat> activeBeats, List<char> inputChars) {
		while (activeBeats.Count > 0) {
			char desiredChar = textManager.GetNextCharacter ();
			bool matchFound = FindMatch (desiredChar, inputChars);
			if (!matchFound)
				return;
			activeBeats.RemoveAt (0);
			if (OnScoreBeat != null)
				OnScoreBeat ();
		}
	}

	bool FindMatch (char desiredChar, List<char> inputChars) {
		for (int i = 0; i < inputChars.Count; i++) {
			if (inputChars[i] == desiredChar) {
				inputChars.RemoveAt (i);
				return true;
			}
		}
		return false;
	}
}
