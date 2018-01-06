using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringChecker {

	TextManager textManager;

	public event System.Action OnScoreBeat = delegate {};

	public ScoringChecker (TextManager textManager) {
		this.textManager = textManager;
	}

	public void CheckForScores (List<Beat> activeBeats, List<char> inputChars) {
		while (activeBeats.Count > 0) {
			Beat beat = activeBeats[0];
			if (beat.textIndex < textManager.textIndex)
				return;
			char desiredChar = textManager.GetCharacterAtIndex (beat.textIndex);
			bool matchFound = FindMatch (desiredChar, inputChars);
			if (!matchFound)
				return;
			textManager.UpdateTextIndex (beat.textIndex);
			activeBeats[0].Destroy ();
			activeBeats.RemoveAt (0);
			OnScoreBeat ();
		}
	}

	bool FindMatch (char desiredChar, List<char> inputChars) {
		Debug.Log (desiredChar);
		for (int i = 0; i < inputChars.Count; i++) {
			if (inputChars[i] == desiredChar) {
				inputChars.RemoveAt (i);
				return true;
			}
		}
		return false;
	}
}
