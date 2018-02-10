using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerBehavior : MonoBehaviour {

	[SerializeField] GameObject menuSystem;
	[SerializeField] GameObject playSystem;
	[SerializeField] GameObject statsSystem;
	[SerializeField] PlayManagerBehavior playManager;
	[SerializeField] StatsSystemBehavior statsManager;

	void Start () {
		LoadMenu ();
	}

	public void LoadMenu () {
		statsSystem.SetActive (false);
		playSystem.SetActive (false);
		menuSystem.SetActive (true);
	}

	public void LoadPlay () {
		statsSystem.SetActive (false);
		menuSystem.SetActive (false);
		playSystem.SetActive (true);
		playManager.Play ();
	}

	public void LoadStats (int totalNumberBeats, int beatsHit, float scorePercentage) {
		menuSystem.SetActive (false);
		playSystem.SetActive (false);
		statsSystem.SetActive (true);
		statsManager.LoadStats (totalNumberBeats, beatsHit, scorePercentage);
	}
}
