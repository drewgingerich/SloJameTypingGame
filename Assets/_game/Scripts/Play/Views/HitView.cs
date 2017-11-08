using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitView : MonoBehaviour {

	[SerializeField] Text textComp;

	public void Wire (BeatActivityMonitor activityMonitor, ScoringChecker scoringChecker) {
		activityMonitor.OnMissedBeat += (_) => DisplayMiss ();
		scoringChecker.OnScoreBeat += DisplayHit;
	}

	void DisplayMiss () {
		textComp.text = "MISS";
		StartCoroutine (FadeText ());
	}

	void DisplayHit () {
		textComp.text = "HIT!";
		StartCoroutine (FadeText ());
	}

	IEnumerator FadeText () {
		yield return new WaitForSeconds (0.2f);
		textComp.text = "";
	}
}
