using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerBehavior : MonoBehaviour {

	MenuSystemBehavior menuSystem;

	void Start () {
		SceneManager.LoadScene ("MenuSystem", LoadSceneMode.Additive);
		menuSystem = GameObject.FindGameObjectWithTag ("MenuSystem").GetComponent<MenuSystemBehavior> ();
		menuSystem.OnStartPlay += StartPlay;
	}

	void StartPlay (SongData songData, List<float> beatmap) {
		SceneManager.UnloadScene ("MenuSystem");
		SceneManager.LoadScene ("Play", LoadSceneMode.Additive);
		
	}
}
