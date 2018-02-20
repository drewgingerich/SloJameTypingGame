using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSystem : MonoBehaviour {

	[SerializeField] GameManager gameManager;
}
// 	[SerializeField] MainMenuBehavior mainMenu;
// 	[SerializeField] SongSelectMenu playSelectMenu;
// 	[SerializeField] BeatmapSelectMenuBehavior playBeatmapSelectMenu;
// 	[SerializeField] SongSelectMenu createSelectMenu;
// 	[SerializeField] BeatmapSelectMenuBehavior createBeatmapSelectMenu;
// 	[SerializeField] ImportMenuBehavior importMenu;
// 	[SerializeField] DesignMenuBehavior designMenu;

// 	void Start () {
// 		Wire ();
// 	}

// 	void OnEnable () {
// 		mainMenu.Load ();
// 	}

// 	void Wire () {
// 		mainMenu.OnChoosePlay += () => {
// 			mainMenu.Unload ();
// 			playSelectMenu.Load ();
// 		};
// 		mainMenu.OnChooseCreate += () => {
// 			mainMenu.Unload ();
// 			createSelectMenu.Load ();
// 		};
// 		mainMenu.OnChooseQuit += () => {
// 			Debug.Log ("Quit");
// 		};
// 		playSelectMenu.OnBack += () => {
// 			playSelectMenu.Unload ();
// 			mainMenu.Load ();
// 		};
// 		playSelectMenu.OnChooseSong += (songData) => {
// 			playSelectMenu.Unload ();
// 			playBeatmapSelectMenu.Load ();
// 		};
// 		playBeatmapSelectMenu.OnBack += () => {
// 			playBeatmapSelectMenu.Unload ();
// 			playSelectMenu.Load ();
// 		};
// 		playBeatmapSelectMenu.OnChooseBlueprint += () => {
// 			playBeatmapSelectMenu.Unload ();
// 			gameManager.LoadPlay ();
// 		};
// 		createSelectMenu.OnBack += () => {
// 			createSelectMenu.Unload ();
// 			mainMenu.Load ();
// 		};
// 		createSelectMenu.OnChooseSong += (songData) => {
// 			createSelectMenu.Unload ();
// 			createBeatmapSelectMenu.Load ();
// 		};
// 		createBeatmapSelectMenu.OnBack += () => {
// 			createBeatmapSelectMenu.Unload ();
// 			playSelectMenu.Load ();
// 		};
// 		createBeatmapSelectMenu.OnChooseBlueprint += () => {
// 			createBeatmapSelectMenu.Unload ();
// 			designMenu.Load ();
// 		};
// 		designMenu.OnBack += () => {
// 			designMenu.Unload ();
// 			createBeatmapSelectMenu.Load ();
// 		};
// 	}
// }