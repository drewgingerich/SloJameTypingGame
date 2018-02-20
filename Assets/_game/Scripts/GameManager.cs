using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	[SerializeField] GameObject startingSceneObject;
	GameObject current;

	public void LoadNext(GameObject next) {
		if (current != null)
			current.SetActive(false);
		current = next;
		next.SetActive(true);
	}

	void Start() {
		LoadNext (startingSceneObject);
	}
}
