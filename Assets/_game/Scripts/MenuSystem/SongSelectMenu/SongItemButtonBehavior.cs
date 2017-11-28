using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SongItemButtonBehavior : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, ISubmitHandler {

	public event System.Action<SongData> OnChoose;

	[SerializeField] Text text;

	Button button;
	SongData songData;

	public void Load (SongData songData) {
		this.songData = songData;
		text.text = songData.songTitle;
	}

	void Awake () {
		button = gameObject.GetComponent<Button> ();
	}

	public void OnPointerEnter (PointerEventData eventData) {
		button.Select ();
	}

	public void OnSelect (BaseEventData eventData) {
		button.image.color = Color.yellow;
	}

	public void OnDeselect (BaseEventData eventData) {
		button.image.color = Color.white;
	}

	public void OnSubmit (BaseEventData eventData) {
		if (OnChoose != null)
			OnChoose (songData);
	}
}
