using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBehavior : MonoBehaviour {

	public event System.Action OnSelectPlay;
	public event System.Action OnSelectCreate;

	MainMenuController controller;

	void Awake () {
		controller = new MainMenuController ();
		controller.Initialize ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Return))
			return;
		else if (Input.GetKeyDown (KeyCode.UpArrow))
			controller.SelectPreviousItem ();
		else if (Input.GetKeyDown (KeyCode.DownArrow))
			controller.SelectNextItem ();
	}

	void HighlightMenuItem (MainMenuController.MenuItem menuItem) {

	}

	void SubmitMenuItem (MainMenuController.MenuItem menuItem) {

	}
}
