using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MusicImporterBehavior : MonoBehaviour {

	[SerializeField] InputField inputField;
	MusicImporter musicImporter;

	public event System.Action OnSuccessImport;
	public event System.Action OnFailImport;

	void Awake () {
		musicImporter = new MusicImporter ();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Return))
			try {
				musicImporter.ImportMusicFile (inputField.text);
				Debug.Log("Succeeded import.");
				if (OnSuccessImport != null)
					OnSuccessImport ();
			}
			catch (FileNotFoundException e) {
				Debug.Log("Failed import.");
				if (OnFailImport != null)
					OnFailImport ();
			}
	}
}
