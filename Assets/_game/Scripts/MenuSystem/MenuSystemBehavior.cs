using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSystemBehavior : MonoBehaviour {

	[SerializeField] MainMenuBehavior mainMenu;
	[SerializeField] PlaySelectMenuBehavior selectPlayMenu;
	[SerializeField] CreateSelectMenuBehavior selectCreateMenu;
	[SerializeField] ImportMenuBehavior musicImporter;
	[SerializeField] DesignMenuBehavior blueprintDesigner;

	GameObject currentMenuObject;

	void Start () {
		Wire ();
		// selectCreateMenu.gameObject.SetActive (true);
		// currentMenuObject = selectCreateMenu.gameObject;
	}

	void Wire () {
		mainMenu.OnChoosePlay += LoadSelectPlayMenu;
		mainMenu.OnChooseCreate += LoadSelectCreateMenu;
		mainMenu.OnChooseQuit += QuitGame;
		selectPlayMenu.OnBack += LoadMainMenu;
		selectCreateMenu.OnSelect += LoadBlueprintBuilder;
		selectCreateMenu.OnBack += LoadMusicImporter;
		musicImporter.OnBack += LoadMusicSelector;
		blueprintDesigner.OnBack += LoadMusicSelector;
	}

	void LoadMainMenu () {

	}

	void LoadSelectPlayMenu () {

	}

	void LoadSelectCreateMenu () {

	}

	void LoadMusicSelector () {
		currentMenuObject.SetActive (false);
		currentMenuObject = selectCreateMenu.gameObject;
		selectCreateMenu.gameObject.SetActive (true);
	}

	void LoadMusicImporter () {
		currentMenuObject.SetActive (false);
		currentMenuObject = musicImporter.gameObject;
		musicImporter.gameObject.SetActive (true);
	}

	void LoadBlueprintBuilder (SongData songData) {
		currentMenuObject.SetActive (false);
		currentMenuObject = blueprintDesigner.gameObject;
		blueprintDesigner.gameObject.SetActive (true);
		blueprintDesigner.Wire (songData);
	}

	void QuitGame () {
		Debug.Log ("Quit");
		//Application.Quit ();
	}
}
