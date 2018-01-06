using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsSystemBehavior : MonoBehaviour {

	[SerializeField] GameManagerBehavior gameManager;
	[SerializeField] ScoreRatioViewBehavior scoreRatioView;
	[SerializeField] ScorePercentageViewBehavior scorePercentageView;

	public void LoadStats (int totalNumberBeats, int beatsHit, float scorePercentage) {
		scoreRatioView.Load (totalNumberBeats, beatsHit);
		scorePercentageView.Load (scorePercentage);
	}

	public void MoveToMenus () {
		gameManager.LoadMenu ();
	}
}
