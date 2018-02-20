using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SongSelectMenu : SnapMenu {

	[SerializeField] GameObject beatmapMenu;
	[SerializeField] GameObject verticalDisplay;
	[SerializeField] GameObject songSelectButtonPrefab;

	List<GameObject> buttons;

	void OnEnable() {
		ClearSongButtons();
		List<SongData> songDatas = SongDataManager.GetSongDataList();
		buttons = new List<GameObject>();
		foreach (SongData data in songDatas) {
			AddSongButton(data);
		}
		if (buttons.Count > 0)
			buttons[0].gameObject.GetComponent<Button>().Select();
	}

	void AddSongButton(SongData songData) {
		GameObject newSongItem = Instantiate(songSelectButtonPrefab);
		newSongItem.transform.SetParent(verticalDisplay.transform);
		Button button = newSongItem.GetComponent<Button>();
		button.GetComponentInChildren<Text>().text = songData.songTitle;
		button.onClick.AddListener(delegate { SelectSong(songData); });
		buttons.Add(newSongItem);
	}

	void ClearSongButtons() {
		if (buttons == null)
			return;
		for (int i = buttons.Count; i > 0; i--)
			Destroy(buttons[i - 1]);
	}

	void Unload() {
		gameObject.SetActive(false);
	}

	public void LoadNext(GameObject next) {
		Unload();
		next.SetActive(true);
	}

	void SelectSong(SongData chosenSong) {
		DataNavigator.currentSong = chosenSong;
		Unload();
		LoadNext(beatmapMenu);
	}
}