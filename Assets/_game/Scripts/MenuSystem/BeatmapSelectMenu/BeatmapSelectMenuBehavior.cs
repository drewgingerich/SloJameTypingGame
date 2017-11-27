using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatmapSelectMenuBehavior : MonoBehaviour {

	public event System.Action OnBack;
	public event System.Action<SongData, List<float>> OnChooseBeatmap;

	[SerializeField] Button backButton;
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
			beatmapItem.OnChooseBlueprint -= ChooseBeatmap;
			Destroy (beatmapItem.gameObject);
		}
	}

	void Awake () {
		backButton.onClick.AddListener ( () => {
			if (OnBack != null) OnBack ();
		});
	}

	void AddBeatmapItem (BeatmapBlueprint blueprint) {
		GameObject newBeatmapItem = Instantiate (beatmapItemPrefab);
		newBeatmapItem.transform.SetParent (verticalDisplay.transform);
		BeatmapItemBehavior itemBehavior = newBeatmapItem.GetComponent<BeatmapItemBehavior> ();
		itemBehavior.Load (blueprint);
		itemBehavior.OnChooseBlueprint += ChooseBeatmap;
		beatmapItems.Add (itemBehavior);
	}

	void ChooseBeatmap (BeatmapBlueprint blueprint) {
		if (OnChooseBeatmap != null) {
			List<float> beatTimings = blueprint.Translate (songData);
			OnChooseBeatmap (songData, beatTimings);
		}
	}
}
