using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent (typeof(Button))]
public class MainMenuButtonBehavior : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler, ISubmitHandler {

	public event System.Action OnChoose;

	Button button;

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
			OnChoose ();
	}
}
