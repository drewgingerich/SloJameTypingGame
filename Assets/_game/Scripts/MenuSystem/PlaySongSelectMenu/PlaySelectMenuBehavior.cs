using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySelectMenuBehavior : MonoBehaviour {

	public event System.Action OnBack;
	public event System.Action<SongData> OnChooseSong;

	[SerializeField] Button backButton;
	[SerializeField] GameObject verticalDisplay;
	[SerializeField] GameObject songItemPrefab;

	List<SongData> songDatas;
	List<SongItemButtonBehavior> songItems;

	public void Load () {
		gameObject.SetActive (true);
		songDatas = SongDataManager.GetSongDataList ();
		songItems = new List<SongItemButtonBehavior> ();
		foreach (SongData data in songDatas) {
			AddSongItem (data);
		}
		songItems[0].gameObject.GetComponent<Button> ().Select ();
	}

	public void Unload () {
		gameObject.SetActive (false);
		foreach (SongItemButtonBehavior songItem in songItems) {
			songItem.OnChoose -= OnChooseSong;
			Destroy (songItem.gameObject);
		}
		for (int i = songItems.Count; i > 0; i--)

			Destroy (songItems[i - 1].gameObject);
	}

	void Awake () {
		backButton.onClick.AddListener ( () => {
			if (OnBack != null) OnBack ();
		});
	}

	void AddSongItem (SongData songData) {
		GameObject newSongItem = Instantiate (songItemPrefab);
		newSongItem.transform.SetParent (verticalDisplay.transform);
		SongItemButtonBehavior itemBehavior = newSongItem.GetComponent<SongItemButtonBehavior> ();
		itemBehavior.Load (songData);
		itemBehavior.OnChoose += OnChooseSong;
		songItems.Add (itemBehavior);
	}
}
