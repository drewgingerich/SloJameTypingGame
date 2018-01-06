using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePercentageViewBehavior : MonoBehaviour {

	[SerializeField] Text text;

	public void Load (float scorePercentage) {
		text.text = string.Format ("Percent hit: {0} %", scorePercentage);
	}
}
