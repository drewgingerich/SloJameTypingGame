using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour {

	[SerializeField] BeatReader beatReader;
	[SerializeField] TextManager textManager;

	public event System.Action OnSuccess;
	public event System.Action OnFailure;

	void Start () {
		beatReader.OnBeat += CheckForInput;
		beatReader.OnNoBeat += CheckForNoInput;
	}

	void CheckForInput() {
		if (Input.anyKeyDown) {
			char expectedChar = textManager.GetCurrentChar ();
			if (Input.inputString.Equals (expectedChar.ToString ())) {
				if (OnSuccess != null) {
					OnSuccess ();
				}
			} else {
				if (OnFailure != null) {
					OnFailure ();
				}
			}
		}
	}

	void CheckForNoInput() {
		if (Input.anyKeyDown) {
			if (OnFailure != null) {
				OnFailure ();
			}
		}
	}
}
