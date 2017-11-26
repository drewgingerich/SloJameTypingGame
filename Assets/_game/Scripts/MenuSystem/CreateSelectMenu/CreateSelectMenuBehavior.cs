using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateSelectMenuBehavior : MonoBehaviour {

	public event System.Action<SongData> OnSelect;
	public event System.Action OnBack;

	// [SerializeField] Button importMusicButton;
	// [SerializeField] GameObject verticalLayout;
	// [SerializeField] GameObject selectionButtonPrefab;

	// List<GameObject> selectionButtonObjects;

	// void Awake () {
	// 	selectionButtonObjects = new List<GameObject> ();
	// 	Wire ();
	// }

	// void Wire () {
	// 	importMusicButton.onClick.AddListener ( () => {
	// 		if (OnBack != null) OnBack ();
	// 	});
	// }

	// void OnEnable () {
	// 	RefreshSelection ();
	// }

	// void RefreshSelection () {
	// 	ClearSelectionButtons ();
	// 	List<SongData> songDataList = SongDataManager.GetSongDataList ();
	// 	foreach (SongData songData in songDataList)
	// 		AddSelectionButton (songData);
	// }

	// void AddSelectionButton (SongData songData) {
	// 	GameObject selectionButtonObject = Instantiate(selectionButtonPrefab, verticalLayout.transform);
	// 	selectionButtonObjects.Add (selectionButtonObject);
	// 	Button selectionButton = selectionButtonObject.GetComponent<Button> ();
	// 	selectionButton.onClick.AddListener ( () => { 
	// 		if (OnSelect != null) OnSelect (songData);
	// 	});
	// 	selectionButton.GetComponentInChildren<Text> ().text = songData.songTitle; 
	// }

	// void ClearSelectionButtons () {
	// 	foreach (GameObject buttonObject in selectionButtonObjects)
	// 		Destroy(buttonObject);
	// 	selectionButtonObjects.Clear ();
	// }
}
