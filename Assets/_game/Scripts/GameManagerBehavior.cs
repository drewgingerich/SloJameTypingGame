using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerBehavior : MonoBehaviour {

	public static GameManagerBehavior instance { get; private set; }

	public event System.Action OnStartSceneTransition = delegate {};
	public event System.Action OnEndSceneTransition = delegate {};

	void Awake () {
		if (instance == null)
			instance = this;
		else
			Debug.LogError ("Multiple GameManagerBehavior instances!");
	}

	IEnumerator Start () {
		OnStartSceneTransition ();
		yield return SceneManager.LoadSceneAsync ("MenuSystem", LoadSceneMode.Additive);
		OnEndSceneTransition ();
	}

	public void StartPlay () {
		StartCoroutine (TransitionToPlay ());
	}

	IEnumerator TransitionToPlay () {
		OnStartSceneTransition ();
		yield return SceneManager.UnloadSceneAsync ("MenuSystem");
		yield return SceneManager.LoadSceneAsync ("Play", LoadSceneMode.Additive);
		OnEndSceneTransition ();
	}

}
