using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBehavior : MonoBehaviour {

	public event System.Action OnChoosePlay;
	public event System.Action OnChooseCreate;
	public event System.Action OnChooseQuit;

	[SerializeField] MainMenuButtonBehavior playButton;
	[SerializeField] MainMenuButtonBehavior createButton;
	[SerializeField] MainMenuButtonBehavior quitButton;

	public void Load () {
		gameObject.SetActive (true);
		playButton.gameObject.GetComponent<Button> ().Select ();
	}

	public void Unload () {
		gameObject.SetActive (false);
	}

	void Start () {
		playButton.OnChoose += () => {
			if (OnChoosePlay != null) OnChoosePlay ();
		};
		createButton.OnChoose += () => {
			if (OnChooseCreate != null) OnChooseCreate ();
		};
		quitButton.OnChoose += () => {
			if (OnChooseQuit != null) OnChooseQuit ();
		};
	}
}
