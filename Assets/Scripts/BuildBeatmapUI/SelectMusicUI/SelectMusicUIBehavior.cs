using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMusicUIBehavior : MonoBehaviour {

	[SerializeField] Button importMusicButton;
	[SerializeField] Button trimMusicButton;
	[SerializeField] Button findBPMButton;
	[SerializeField] Button buildBeatmapButton;
	[SerializeField] GameObject verticalLayout;
	[SerializeField] GameObject selectionButtonPrefab;

	List<GameObject> selectionButtonObjects;
	Button lastSelectedButton;
	string currentSongTitle;

	public event System.Action OnSelectImportMusic;
	public event System.Action<string> OnSelectTrimMusic;
	public event System.Action<string> OnSelectFindBPM;
	public event System.Action<string> OnSelectBuildBeatmap;

	void Awake () {
		selectionButtonObjects = new List<GameObject> ();
		WireUIComponents ();
	}

	void WireUIComponents () {
		importMusicButton.onClick.AddListener ( () => {
			if (OnSelectImportMusic != null) OnSelectImportMusic ();
		});
		trimMusicButton.onClick.AddListener ( () => {
			if (OnSelectTrimMusic != null) OnSelectTrimMusic (currentSongTitle);
		});
		findBPMButton.onClick.AddListener ( () => {
			if (OnSelectFindBPM != null) OnSelectFindBPM (currentSongTitle);
		});
		buildBeatmapButton.onClick.AddListener ( () => {
			if (OnSelectBuildBeatmap != null) OnSelectBuildBeatmap (currentSongTitle);
		});
	}

	void OnEnable () {
		RefreshSelection ();
	}

	void RefreshSelection () {
		ClearSelectionButtons ();
		string[] songTitles = SongImportManager.GetImportedSongTitles ();
		foreach (string trackTitle in songTitles)
			AddSelectionButton (trackTitle);
		currentSongTitle = songTitles[0];
		lastSelectedButton = selectionButtonObjects [0].GetComponent<Button> ();
		lastSelectedButton.interactable = false;
	}

	void AddSelectionButton (string trackTitle) {
		GameObject selectionButtonObject = Instantiate(selectionButtonPrefab, verticalLayout.transform);
		selectionButtonObjects.Add (selectionButtonObject);
		Button selectionButton = selectionButtonObject.GetComponent<Button> ();
		selectionButton.onClick.AddListener ( () => { SelectTrack (trackTitle, selectionButton); } );
		selectionButton.GetComponentInChildren<Text> ().text = trackTitle; 
	}

	void SelectTrack (string trackTitle, Button pressedButton) {
		currentSongTitle = trackTitle;
		lastSelectedButton.interactable = true;
		pressedButton.interactable = false;
		lastSelectedButton = pressedButton;
	}

	void ClearSelectionButtons () {
		foreach (GameObject buttonObject in selectionButtonObjects)
			Destroy(buttonObject);
		selectionButtonObjects.Clear ();
	}
}
