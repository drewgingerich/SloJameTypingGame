using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputChecker : MonoBehaviour {

	[SerializeField] GameFlowManager gameEventManager;
	[SerializeField] InteractiveBeatActivityTracker interactiveBeatActivityTracker;
	[SerializeField] TextManager textManager;

	public event System.Action<InteractiveBeat> OnCorrectCharacter;
//	public event System.Action<InteractiveBeat> OnIncorrectCharacter;

	void Start () {
		gameEventManager.OnHandleInput += CheckForCorrectInput;
	}

	void CheckForCorrectInput () {
		List<InteractiveBeat> activeBeats = interactiveBeatActivityTracker.ActiveInteractiveBeats;
		if (activeBeats.Count == 0 || Input.inputString.Length == 0)
			return;
		foreach (char inputChar in Input.inputString) {
			for (int i = 0; i < activeBeats.Count; i++) {
				char desiredChar = textManager.GetUpcomingCharacter (i);
				if (inputChar == desiredChar) {
					Debug.Log ("correct");
					OnCorrectCharacter (activeBeats [i]);
					activeBeats [i].Destroy ();
					activeBeats.RemoveAt (i);
					break;
				}
			}
		}
	}
}

// More sophisticate but premature matching system.

//	void HandleInput () {
//		List<InteractiveBeat> activeBeats = interactiveBeatActivityTracker.ActiveInteractiveBeats;
//		if (activeBeats.Count == 0 || Input.inputString.Length == 0)
//			return;
//		
//		int activeBeatIndex = 0;
//		foreach (char inputChar in Input.inputString) {
//			int? firstMatchIndex = GetFirstMatchIndex (inputChar, activeBeats, activeBeatIndex);
//			if (firstMatchIndex == null)
//				continue;
//			OnCorrectCharacter (activeBeats [firstMatchIndex]);
//			int indexDiff = firstMatchIndex - activeBeatIndex;
//			foreach (InteractiveBeat beat in activeBeats.GetRange (activeBeats, indexDiff)) {
//				OnIncorrectCharacter (beat);
//			}
//			activeBeatIndex = firstMatchIndex + 1;
//			if (activeBeatIndex == activeBeats.Count - 1)
//				return;
//		}
//	}
//
//	int? GetFirstMatchIndex (char inputChar, List<InteractiveBeat> activeBeats, int startIndex) {
//		char desiredChar;
//		for (int i = startIndex; i < activeBeats.Count; i++) {
//			desiredChar = textManager.GetUpcomingCharacter (i);
//			if (inputChar = desiredChar)
//				return i;
//		}
//		return null;
//	}
