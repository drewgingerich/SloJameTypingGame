using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BeatmapSelectMenu : MonoBehaviour {

	public UnityEvent OnSelectBeatmap;

	[SerializeField] Button addButton;
	[SerializeField] GameObject verticalLayout;
	[SerializeField] GameObject blueprintButtonPrefab;

	List<GameObject> buttons;

	public void AddBlueprint() {
		BeatmapBlueprint newBlueprint = new BeatmapBlueprint();
		DataNavigator.currentSong.blueprints.Add(newBlueprint);
		GenerateBeatmapSelectButtons ();
	}

	public void SelectBlueprint(BeatmapBlueprint blueprint) {
		DataNavigator.currentBlueprint = blueprint;
		OnSelectBeatmap.Invoke();
	}

	void OnEnable() {
		GenerateBeatmapSelectButtons ();
	}

	void GenerateBeatmapSelectButtons () {
		ClearSongButtons();
		SongData songData = DataNavigator.GetCurrentSongData();;
		if (songData.blueprints.Count == 0) {
			addButton.Select();
			return;
		}
		buttons = new List<GameObject>();
		for (int i = 0; i < songData.blueprints.Count; i++) {
			AddBeatmapSelectButton(songData.blueprints[i], i);
		}
		buttons[0].gameObject.GetComponent<Button>().Select();
	}

	void AddBeatmapSelectButton(BeatmapBlueprint blueprint, int blueprintIndex) {
		GameObject newButtonObject = Instantiate(blueprintButtonPrefab);
		newButtonObject.transform.SetParent(verticalLayout.transform);
		Button newButton = newButtonObject.GetComponent<Button>();
		newButton.GetComponentInChildren<Text>().text = string.Format("Blueprint {0}", blueprintIndex);
		newButton.onClick.AddListener(delegate { SelectBlueprint(blueprint); });
		buttons.Add(newButtonObject);
	}

	void ClearSongButtons() {
		if (buttons == null)
			return;
		for (int i = buttons.Count; i > 0; i--)
			Destroy(buttons[i - 1]);
	}
}
