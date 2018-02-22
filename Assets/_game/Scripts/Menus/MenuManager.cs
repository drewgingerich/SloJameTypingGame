using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	[SerializeField] GameObject startingMenuObject;
	GameObject currentMenuObject;

	public void LoadNext(GameObject next) {
		if (currentMenuObject != null)
			currentMenuObject.SetActive(false);
		currentMenuObject = next;
		next.SetActive(true);
	}

	void Start() {
		LoadNext (startingMenuObject);
	}
}
