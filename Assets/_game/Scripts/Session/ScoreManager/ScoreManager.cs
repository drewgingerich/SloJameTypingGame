using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager {

	TextManager textManager;

	public event System.Action OnScoreBeat;

	public ScoreManager (TextManager textManager) {
		this.textManager = textManager;
	}

	public void UpdateScore (int numberOfActiveBeats, List<char> inputChars) {
		while (numberOfActiveBeats > 0) {
			char desiredChar = textManager.GetNextCharacter ();
			bool matchFound = FindMatch (desiredChar, inputChars);
			if (!matchFound)
				return;
			numberOfActiveBeats--;
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
