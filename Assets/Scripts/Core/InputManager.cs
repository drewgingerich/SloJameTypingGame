using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	// Connect in Unity Inspector.
	[SerializeField] GameEventManager gameEventManager;
	[SerializeField] InteractiveBeatSpawner interactiveBeatSpawner;
	[SerializeField] TextManager TextManager;

	List<InteractiveBeat> activeBeats;

	void Awake () {
		activeBeats = new List<InteractiveBeat> ();
	}

	void Start () {
		gameEventManager.OnHandleInput += HandleInput;
		interactiveBeatSpawner.OnCreateInteractiveBeat += MonitorInteractiveBeat;
	}
	
	void HandleInput () {
		if (activeBeats.Count == 0) {
			return;
		}
		if (string.IsNullOrEmpty (Input.inputString)) {
			return;
		}
		foreach (char inputChar in Input.inputString) {
			InteractiveBeat activeBeat;
			char desiredChar;
			for (int i = 1; i < activeBeats.Count; i++) {
				activeBeat = activeBeats [i];
				desiredChar = TextManager.GetCurrentCharAtIndex (i);
				if (inputChar == desiredChar) {
					activeBeat.Success ();
					activeBeats.RemoveAt (i);
				} else {
					if (i == 0) {
						activeBeat.Failure ();
						activeBeats.RemoveAt (i);
					}
				}
			}
		}
	}

	void MonitorInteractiveBeat (InteractiveBeat interactiveBeat) {
		interactiveBeat.OnEnterActiveWindow += ( activeBeat => activeBeats.Add (activeBeat));
		interactiveBeat.OnExitActiveWindow += ( activeBeat => activeBeats.Remove (activeBeat));
	}
}
