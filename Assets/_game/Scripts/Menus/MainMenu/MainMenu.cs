using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : SnapMenu {

	[SerializeField] GameObject firstSelectedButton;

	void OnEnable() {
		firstSelectedButton.GetComponent<Button>().Select();
	}

	void Unload() {
		gameObject.SetActive(false);
	}

	public void LoadNext(GameObject next) {
		next.SetActive(true);
		Unload();
	}
}
