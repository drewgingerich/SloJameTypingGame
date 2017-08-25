using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootUIControllerBehavior : MonoBehaviour {

	[SerializeField] SelectMusicUIBehavior musicSelector;
	[SerializeField] ImportMusicUIBehavior musicImporter;
	// [SerializeField] TrimMusicUIBehavior musicTrimmer;

	GameObject currentMenuObject;

	void Start () {
		SongImportManager.EnsureStorageDirectoryExists ();
		WireUIComponents ();
		musicSelector.gameObject.SetActive (true);
		currentMenuObject = musicSelector.gameObject;
	}

	void WireUIComponents () {
		musicSelector.OnSelectImportMusic += LoadImportUI;
		// musicSelector.OnSelectTrimMusic += LoadTrimUI;
		musicImporter.OnBack += LoadSelectUI;
		// musicTrimmer.OnBack += LoadSelectUI;
	}

	void LoadSelectUI () {
		currentMenuObject.SetActive (false);
		currentMenuObject = musicSelector.gameObject;
		musicSelector.gameObject.SetActive (true);
	}

	void LoadImportUI () {
		currentMenuObject.SetActive (false);
		currentMenuObject = musicImporter.gameObject;
		musicImporter.gameObject.SetActive (true);
	}

	void LoadBuildUI () {
		currentMenuObject.SetActive (false);
		currentMenuObject = musicImporter.gameObject;
		musicImporter.gameObject.SetActive (true);
	}

	// void LoadTrimUI (string filePath) {
	// 	currentMenuObject.SetActive (false);
	// 	currentMenuObject = musicTrimmer.gameObject;
	// 	musicTrimmer.gameObject.SetActive (true);
	// 	musicTrimmer.LoadMusicTrack (filePath);
	// }

	// void ActivateBuildUI (string filePath) {
	// 	currentMenuObject.SetActive (false);
	// }
}
