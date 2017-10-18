using UnityEngine.UI;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUIBehavior : MonoBehaviour {

	[SerializeField] InteractiveBeatSystemBehavior beatSystem;
	[SerializeField] Text textComp;

	ScoreKeeper scoreKeeper;

	void Start () {
		scoreKeeper = beatSystem.ScoreKeeper;
		scoreKeeper.OnScoreBeat += UpdateScoreUI;
	}

	void UpdateScoreUI (int score, int combo, ScoreKeeper.ScoreCategory scoreCategory, InteractiveBeat scoredBeat) {
		StringBuilder stringBuilder = new StringBuilder ();
		stringBuilder.Append (score.ToString ());
		stringBuilder.Append ('\n');
		stringBuilder.Append (combo.ToString ());
		stringBuilder.Append ('\n');
		stringBuilder.Append (scoreCategory.ToString ());
		stringBuilder.Append ('\n');
		stringBuilder.Append (scoredBeat.TemporalDistance.ToString ());
		textComp.text = stringBuilder.ToString ();
	}
}
