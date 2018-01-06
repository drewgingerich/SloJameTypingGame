using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRatioViewBehavior : MonoBehaviour {

	[SerializeField] Text text;

	public void Load (int totalNumberBeats, int beatsHit) {
		text.text = string.Format ("Beats hit: {0} / {1}", beatsHit, totalNumberBeats);
	}
}
