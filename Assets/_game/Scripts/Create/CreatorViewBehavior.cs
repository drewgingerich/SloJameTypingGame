using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorViewBehavior : MonoBehaviour {

	[SerializeField] SelectMusicUIBehavior musicSelector;
	[SerializeField] ImportMusicUIBehavior musicImporter;
	[SerializeField] BlueprintDesignerViewBehavior blueprintDesigner;

	GameObject currentMenuObject;

	void Start () {
		SongImportManager.EnsureStorageDirectoryExists ();
		Wire ();
		musicSelector.gameObject.SetActive (true);
		currentMenuObject = musicSelector.gameObject;
	}

	void Wire () {
		musicSelector.OnSelectSong += LoadBlueprintBuilder;
		musicSelector.OnSelectImport += LoadMusicImporter;
		musicImporter.OnBack += LoadMusicSelector;
		blueprintDesigner.OnBack += LoadMusicSelector;
	}

	void LoadMusicSelector () {
		currentMenuObject.SetActive (false);
		currentMenuObject = musicSelector.gameObject;
		musicSelector.gameObject.SetActive (true);
	}

	void LoadMusicImporter () {
		currentMenuObject.SetActive (false);
		currentMenuObject = musicImporter.gameObject;
		musicImporter.gameObject.SetActive (true);
	}

	void LoadBlueprintBuilder (string songTitle) {
		currentMenuObject.SetActive (false);
		currentMenuObject = blueprintDesigner.gameObject;
		blueprintDesigner.gameObject.SetActive (true);
		blueprintDesigner.Wire (songTitle);
	}
}
