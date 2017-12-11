using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatmapSelectMenuBehavior : MonoBehaviour {

	public event System.Action OnBack;
	public event System.Action<SongData, BeatmapBlueprint> OnChooseBlueprint;

	[SerializeField] Button backButton;
	[SerializeField] Button addBlueprintButton;
	[SerializeField] GameObject verticalDisplay;
	[SerializeField] GameObject beatmapItemPrefab;

	SongData songData;
	List<BeatmapBlueprint> blueprints;
	List<BeatmapItemBehavior> beatmapItems;

	public void Load (SongData songData) {
		this.songData = songData;
		gameObject.SetActive (true);
		beatmapItems = new List<BeatmapItemBehavior> ();
		foreach (BeatmapBlueprint blueprint in songData.blueprints) {
			AddBeatmapItem (blueprint);
		}
		if (beatmapItems.Count > 0)
			beatmapItems[0].gameObject.GetComponent<Button> ().Select ();
	}

	public void Unload () {
		gameObject.SetActive (false);
		foreach (BeatmapItemBehavior beatmapItem in beatmapItems) {
			beatmapItem.OnChooseBlueprint -= ChooseBlueprint;
			Destroy (beatmapItem.gameObject);
		}
	}

	void Awake () {
		backButton.onClick.AddListener ( () => {
			if (OnBack != null) OnBack ();
		});
		if (addBlueprintButton != null) {
			addBlueprintButton.onClick.AddListener (AddBlueprint);
		}
	}

	void AddBeatmapItem (BeatmapBlueprint blueprint) {
		GameObject newBeatmapItem = Instantiate (beatmapItemPrefab);
		newBeatmapItem.transform.SetParent (verticalDisplay.transform);
		BeatmapItemBehavior itemBehavior = newBeatmapItem.GetComponent<BeatmapItemBehavior> ();
		itemBehavior.Load (blueprint);
		itemBehavior.OnChooseBlueprint += ChooseBlueprint;
		beatmapItems.Add (itemBehavior);
	}

	void AddBlueprint () {
		BeatmapBlueprint newBlueprint = new BeatmapBlueprint ();
		songData.blueprints.Add (newBlueprint);
		AddBeatmapItem (newBlueprint);
	}

	void ChooseBlueprint (BeatmapBlueprint blueprint) {
		if (OnChooseBlueprint != null)
			OnChooseBlueprint (songData, blueprint);
	}
}
