using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatInfoUI : MonoBehaviour {

	[SerializeField] Text beatInfoText;

	int beatValue;
	int beatIndex;

	public void UpdateBeatValue(int newValue) {
		beatValue = newValue;
		DisplayBeatInfo();
	}

	public void UpdateBeatIndex(int newIndex) {
		beatIndex = newIndex;
		DisplayBeatInfo();
	}

	void DisplayBeatInfo() {
		int beatValueMeasureFraction = BeatmapBlueprint.measureDivisor / beatValue;
		int beatNumber = 1 + beatIndex / beatValue;
		beatInfoText.text = string.Format("{0}/{1}", beatNumber.ToString(), beatValueMeasureFraction.ToString());
	}
}
