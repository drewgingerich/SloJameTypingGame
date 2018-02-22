using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	[SerializeField] GameObject startingDomainObject;
	GameObject currentDomainObject;

	public void LoadNext(GameObject next) {
		if (currentDomainObject != null)
			currentDomainObject.SetActive(false);
		currentDomainObject = next;
		next.SetActive(true);
	}

	void Start() {
		LoadNext (startingDomainObject);
	}
}
