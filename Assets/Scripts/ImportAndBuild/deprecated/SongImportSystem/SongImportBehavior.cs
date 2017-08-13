using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongImportBehavior : MonoBehaviour {

	DirectoryDetective directoryDetective;

	void Awake () {
		directoryDetective = new DirectoryDetective (System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic));
	}
}
