using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

	[SerializeField] InteractiveBeatActivityTracker interactiveBeatActivityTracker;
	[SerializeField] InputChecker inputChecker;

	public int Score { get; private set; }
	public int Combo { get; private set; }
	public bool ComboBroken { get; private set; }

	public event System.Action<float, int> OnScoreCorrectCharacter;
	public event System.Action OnScoreMissedCharacter;


	void Awake () {
		Combo = 0;
		ComboBroken = false;
		Score = 0;
	}

	void Start () {
		interactiveBeatActivityTracker.OnDeactivateInteractiveBeat += ScoreMissedCharacter;
		inputChecker.OnCorrectCharacter += ScoreCorrectCharacter;
	}

	void ScoreCorrectCharacter (InteractiveBeat interactiveBeat) {
		Combo++;
		Score += Combo * (int)Mathf.Log (Combo) * 9;
		if (OnScoreCorrectCharacter != null)
			OnScoreCorrectCharacter (interactiveBeat.TemporalDistanceFromBeat, Combo);
	}

	void ScoreMissedCharacter (InteractiveBeat interactiveBeat) {
		Combo = 0;
		ComboBroken = true;
		OnScoreMissedCharacter ();
	}

}
