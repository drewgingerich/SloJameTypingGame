using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongItemBehavior : MonoBehaviour {

	[SerializeField] Text text;

	public void Load (SongData songData) {
		text.text = songData.songTitle;
	}

	public void Select () {
		text.color = Color.yellow;
	}

	public void Deselect () {
		text.color = Color.white;
	}
}
