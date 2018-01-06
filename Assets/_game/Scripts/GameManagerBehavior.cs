using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerBehavior : MonoBehaviour {

	[SerializeField] GameObject MenuSystem;
	[SerializeField] GameObject PlaySystem;
	[SerializeField] PlayManagerBehavior PlayManager;

	void Start () {
		LoadMenu ();
	}

	public void LoadMenu () {
		PlaySystem.SetActive (false);
		MenuSystem.SetActive (true);
	}

	public void LoadPlay () {
		MenuSystem.SetActive (false);
		PlaySystem.SetActive (true);
		PlayManager.Play ();
	}
}
