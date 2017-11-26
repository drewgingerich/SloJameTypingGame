using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySelectMenuBehavior : MonoBehaviour {

	public event System.Action OnBack;
	public event System.Action<SongData> OnChooseSong;

	[SerializeField] GameObject verticalDisplay;
	[SerializeField] GameObject songItemPrefab;

	List<SongData> songDatas;
	List<SongItemBehavior> songItems;
	int itemIndex = 0;

	public void LoadSongDatas (List<SongData> songDatas) {
		this.songDatas = songDatas;
		foreach (SongData data in songDatas) {
			AddSongItem (data);
		}
	}

	void ShiftItemIndex (int indexShift) {
		songItems[itemIndex].Deselect ();
		itemIndex = (itemIndex + indexShift) % songDatas.Count;
		songItems[itemIndex].Select ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			if (OnChooseSong != null)
				OnChooseSong (songDatas[itemIndex]);
		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			ShiftItemIndex (-1);
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			ShiftItemIndex (1);
		}
	}

	void AddSongItem (SongData songData) {
		GameObject newSongItem = Instantiate (songItemPrefab);
		newSongItem.transform.parent = verticalDisplay.transform;
		SongItemBehavior itemBehavior = newSongItem.GetComponent<SongItemBehavior> ();
		itemBehavior.Load (songData);
		songItems.Add (itemBehavior);
	}
}
