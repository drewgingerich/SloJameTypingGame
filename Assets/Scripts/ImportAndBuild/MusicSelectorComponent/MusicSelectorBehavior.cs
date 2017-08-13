using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MusicSelectorBehavior : MonoBehaviour {

	[SerializeField] GameObject verticalLayout;
	[SerializeField] GameObject buttonPrefab;

	MusicSelector musicSelector;
	List<GameObject> selectionButtons;

	public event System.Action<string> OnSelectFile;

	void Awake () {
		musicSelector = new MusicSelector ();
		selectionButtons = new List<GameObject> ();
	}

	void Start () {
		UpdateFileList ();
	}

	void UpdateFileList () {
		ClearSelectionButtons ();
		string[] musicFilePaths = musicSelector.GetMusicFilenames ();
		foreach (string filePath in musicFilePaths) {
			Button buttonComponent = CreateSelectionButton ();
			buttonComponent.onClick.AddListener ( () => {
				if (OnSelectFile != null) OnSelectFile (filePath);
			} );
			SetButtonText (buttonComponent, filePath);
		}
	}

	Button CreateSelectionButton () {
		GameObject newFilenameButton = Instantiate(buttonPrefab, verticalLayout.transform);
		selectionButtons.Add (newFilenameButton);
		return newFilenameButton.GetComponent<Button> ();
	}

	void SetButtonText (Button buttonComponent, string filePath) {
		GameObject textObject = buttonComponent.transform.GetChild (0).gameObject;
		Text buttonTextComponent = textObject.GetComponent<Text> ();
		buttonTextComponent.text = Path.GetFileNameWithoutExtension (filePath);
	}

	void ClearSelectionButtons () {
		foreach (GameObject buttonObject in selectionButtons)
			Destroy(buttonObject);
		selectionButtons.Clear ();
	}
}
