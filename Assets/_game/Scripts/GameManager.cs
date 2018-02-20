using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	[SerializeField] GameObject startingSceneObject;

	void Start() {
		startingSceneObject.SetActive(true);
	}
}
