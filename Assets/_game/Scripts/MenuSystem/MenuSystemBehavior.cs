using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSystemBehavior : MonoBehaviour {

	public event System.Action<SongData, List<float>> OnStartPlay;

	[SerializeField] MainMenuBehavior mainMenu;
	[SerializeField] PlaySelectMenuBehavior playSelectMenu;
	[SerializeField] BeatmapSelectMenuBehavior beatmapSelectMenu;
	[SerializeField] CreateSelectMenuBehavior createSelectMenu;
	[SerializeField] ImportMenuBehavior importMenu;
	[SerializeField] DesignMenuBehavior designMenu;

	void Start () {
		Wire ();
		playSelectMenu.Load ();
	}

	void Wire () {
		mainMenu.OnChoosePlay += () => {
			mainMenu.Unload ();
			playSelectMenu.Load ();
		};
		mainMenu.OnChooseCreate += () => {
			mainMenu.Unload ();
			createSelectMenu.Load ();
		};
		mainMenu.OnChooseQuit += () => {
			Debug.Log ("Quit");
		};
		playSelectMenu.OnBack += () => {
			playSelectMenu.Unload ();
			mainMenu.Load ();
		};
		playSelectMenu.OnChooseSong += (songData) => {
			playSelectMenu.Unload ();
			beatmapSelectMenu.Load (songData);
		};
		beatmapSelectMenu.OnBack += () => {
			beatmapSelectMenu.Unload ();
			playSelectMenu.Load ();
		};
		beatmapSelectMenu.OnChooseBeatmap += (songData, beatmap) => {
			beatmapSelectMenu.Unload ();
			Debug.Log ("OnStartPlay");
			if (OnStartPlay != null)
				OnStartPlay (songData, beatmap);
		};
	}
}