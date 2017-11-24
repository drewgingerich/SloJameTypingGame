using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImportMenuBehavior : MonoBehaviour {

	[SerializeField] Button backButton;
	[SerializeField] Button importButton;
	[SerializeField] InputField inputField;
	[SerializeField] Text importFailedText;
	[SerializeField] Text importSucceededText;

	public event System.Action OnBack;

	void Start () {
		backButton.onClick.AddListener ( () => { if (OnBack != null) OnBack (); } );
		importButton.onClick.AddListener ( () => { ImportMusicFile (inputField.text); } );
	}

	void OnEnable () {
		importSucceededText.gameObject.SetActive (false);
		inputField.text = "";
	}

	void ImportMusicFile (string filePath) {
		bool importSuccess;
		try {
			//SongImportManager.ImportSong (filePath);
			importSuccess = true;
		} catch (System.Exception e) {
			Debug.LogError (e);
			importSuccess = false;
		}
		importSucceededText.gameObject.SetActive (importSuccess);
		importFailedText.gameObject.SetActive (!importSuccess);
		inputField.text = string.Empty;
	}
}
