using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatmapSelectMenu : SnapMenu {

	[SerializeField] GameObject nextMenu;
	[SerializeField] Button addButton;
	[SerializeField] GameObject verticalDisplay;
	[SerializeField] GameObject buttonPrefab;

	List<BeatmapBlueprint> blueprints;
	List<GameObject> buttons;

	public void AddBlueprint() {
		BeatmapBlueprint newBlueprint = new BeatmapBlueprint();
		DataNavigator.currentSong.blueprints.Add(newBlueprint);
		AddBeatmapSelectButton(newBlueprint);
		buttons[buttons.Count - 1].GetComponent<Button>().Select();
	}

	public void SelectBlueprint(BeatmapBlueprint blueprint) {
		DataNavigator.currentBlueprint = blueprint;
		LoadNext(nextMenu);
	}

	public void LoadNext(GameObject next) {
		Unload();
		next.SetActive(true);
	}

	void OnEnable() {
		ClearSongButtons();
		SongData songData = DataNavigator.GetCurrentSongData();;
		buttons = new List<GameObject>();
		foreach (BeatmapBlueprint blueprint in songData.blueprints) {
			AddBeatmapSelectButton(blueprint);
		}
		if (buttons.Count > 0)
			buttons[0].gameObject.GetComponent<Button>().Select();
		else
			addButton.Select();
	}

	void AddBeatmapSelectButton(BeatmapBlueprint blueprint) {
		GameObject newButtonObject = Instantiate(buttonPrefab);
		newButtonObject.transform.SetParent(verticalDisplay.transform);
		Button newButton = newButtonObject.GetComponent<Button>();
		newButton.GetComponentInChildren<Text>().text = "Blueprint";
		newButton.onClick.AddListener(delegate { SelectBlueprint(blueprint); });
		buttons.Add(newButtonObject);
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

	void ClearButtons() {
		if (buttons == null)
			return;
		for (int i = buttons.Count; i > 0; i--)
			Destroy(buttons[i - 1]);
	}
}
