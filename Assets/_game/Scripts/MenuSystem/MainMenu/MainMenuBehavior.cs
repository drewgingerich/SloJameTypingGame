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
		playButton.gameObject.GetComponent<Button> ().Select ();
	}
}
