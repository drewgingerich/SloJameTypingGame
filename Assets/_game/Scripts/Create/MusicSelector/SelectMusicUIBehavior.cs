using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMusicUIBehavior : MonoBehaviour {

	[SerializeField] Button importMusicButton;
	[SerializeField] GameObject verticalLayout;
	[SerializeField] GameObject selectionButtonPrefab;

	List<GameObject> selectionButtonObjects;

	public event System.Action<string> OnSelectSong;
	public event System.Action OnSelectImport;

	void Awake () {
		selectionButtonObjects = new List<GameObject> ();
		WireUIComponents ();
	}

	void WireUIComponents () {
		importMusicButton.onClick.AddListener ( () => {
			if (OnSelectImport != null) OnSelectImport ();
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
	}

	void AddSelectionButton (string songTitle) {
		GameObject selectionButtonObject = Instantiate(selectionButtonPrefab, verticalLayout.transform);
		selectionButtonObjects.Add (selectionButtonObject);
		Button selectionButton = selectionButtonObject.GetComponent<Button> ();
		selectionButton.onClick.AddListener ( () => { 
			if (OnSelectSong != null) OnSelectSong (songTitle);
		});
		selectionButton.GetComponentInChildren<Text> ().text = songTitle; 
	}

	void ClearSelectionButtons () {
		foreach (GameObject buttonObject in selectionButtonObjects)
			Destroy(buttonObject);
		selectionButtonObjects.Clear ();
	}
}
