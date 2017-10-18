using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatInfoViewBehavior : MonoBehaviour {

	[SerializeField] Text beatInfoText;

	BlueprintDesigner.BeatValue beatValue;

	public void Wire (BlueprintDesigner designer) {
		designer.OnShiftBeatValue += UpdateBeatValue;
		designer.OnShiftBeat += UpdateBeatIndex;
	}

	void UpdateBeatValue (BlueprintDesigner.BeatValue beatValue) {
		this.beatValue = beatValue;
	}

	void UpdateBeatIndex (int index) {
		int beatValueMeasureFraction = BeatMapBlueprint.measureDivisor / (int)beatValue;
		int beatNumber = 1 + index / (int)beatValue;
		beatInfoText.text = string.Format ("{0}/{1}", beatNumber.ToString (), beatValueMeasureFraction.ToString ());
	}
}
