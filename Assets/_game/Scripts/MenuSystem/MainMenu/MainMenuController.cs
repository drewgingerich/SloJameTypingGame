using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController {

	public enum MenuItem { Play, Create }

	public event System.Action<MenuItem> OnSelectMenuItem;

	int selectionIndex = 0;

	public void Initialize () {
		if (OnSelectMenuItem != null)
			OnSelectMenuItem ((MenuItem)selectionIndex);
	}

	public void SelectNextItem () {

	}

	public void SelectPreviousItem () {

	}
}
