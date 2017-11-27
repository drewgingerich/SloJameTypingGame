using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BeatmapItemBehavior : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler, ISubmitHandler {

	public event System.Action<BeatmapBlueprint> OnChooseBlueprint;

	Button button;
	BeatmapBlueprint blueprint;

	void Awake () {
		button = gameObject.GetComponent<Button> ();
	}

	public void Load (BeatmapBlueprint blueprint) {
		this.blueprint = blueprint;
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
		if (OnChooseBlueprint != null)
			OnChooseBlueprint (blueprint);
	}
}

