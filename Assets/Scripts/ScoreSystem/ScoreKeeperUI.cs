using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeperUI : MonoBehaviour {

	[SerializeField] ScoreKeeper scoreKeeper;
	[SerializeField] Text textComp;

	void Start () {
		scoreKeeper.OnScoreCorrectCharacter += DisplayScoreCorrectCharacter;
		scoreKeeper.OnScoreMissedCharacter += DisplayScoreMissedCharacter;
	}

	void DisplayScoreCorrectCharacter (float temporalDistance, int combo) {

	}

	void DisplayScoreMissedCharacter () {

	}
}
