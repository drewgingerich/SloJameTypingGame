using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSystemBehavior : MonoBehaviour {

	[SerializeField] GameManagerBehavior gameManager;
	[SerializeField] MainMenuBehavior mainMenu;
	[SerializeField] SongSelectMenuBehavior playSelectMenu;
	[SerializeField] BeatmapSelectMenuBehavior playBeatmapSelectMenu;
	[SerializeField] SongSelectMenuBehavior createSelectMenu;
	[SerializeField] BeatmapSelectMenuBehavior createBeatmapSelectMenu;
	[SerializeField] ImportMenuBehavior importMenu;
	[SerializeField] DesignMenuBehavior designMenu;

	void Start () {
		Wire ();
	}

	void OnEnable () {
		mainMenu.Load ();
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
			playBeatmapSelectMenu.Load (songData);
		};
		playBeatmapSelectMenu.OnBack += () => {
			playBeatmapSelectMenu.Unload ();
			playSelectMenu.Load ();
		};
		playBeatmapSelectMenu.OnChooseBlueprint += (songData, beatmap) => {
			playBeatmapSelectMenu.Unload ();
			gameManager.LoadPlay ();
		};
		createSelectMenu.OnBack += () => {
			createSelectMenu.Unload ();
			mainMenu.Load ();
		};
		createSelectMenu.OnChooseSong += (songData) => {
			createSelectMenu.Unload ();
			createBeatmapSelectMenu.Load (songData);
		};
		createBeatmapSelectMenu.OnBack += () => {
			createBeatmapSelectMenu.Unload ();
			playSelectMenu.Load ();
		};
		createBeatmapSelectMenu.OnChooseBlueprint += (songData, beatmap) => {
			createBeatmapSelectMenu.Unload ();
			designMenu.Load (songData, beatmap);
		};
	}
}