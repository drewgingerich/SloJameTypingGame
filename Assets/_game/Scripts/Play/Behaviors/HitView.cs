using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitView : MonoBehaviour {

	[SerializeField] Text textComp;
	[SerializeField] new ParticleSystem particleSystem;

	public void Wire (BeatActivityMonitor activityMonitor, ScoringChecker scoringChecker, ParticleSystem particleSystem) {
		activityMonitor.OnMissedBeat += DisplayMiss;
		scoringChecker.OnScoreBeat += DisplayHit;
		this.particleSystem = particleSystem;
	}

	void DisplayMiss () {
		textComp.text = "MISS";
		StartCoroutine (FadeText ());
	}

	void DisplayHit () {
		textComp.text = "HIT!";
		particleSystem.Play ();
		StartCoroutine (FadeText ());
	}

	IEnumerator FadeText () {
		yield return new WaitForSeconds (0.2f);
		textComp.text = "";
	}
}
