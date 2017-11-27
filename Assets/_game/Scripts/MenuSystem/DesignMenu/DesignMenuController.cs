using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DesignMenuController {

	public enum BeatValue {
		Sixtyfourth = 3,
		Fourtyeighth = 4,
		Thirtysecond = 6,
		Twentyfourth = 8,
		Sixteenth = 12,
		Twelvth = 16,
		Eighth = 24,
		Sixth = 32,
		Quarter = 48,
		Third = 64,
	}

	BeatValue beatValue;
	BeatmapBlueprint blueprint;
	int measureIndex;
	bool[] measure;
	int beatIndex;

	public event System.Action<int> OnShiftBeat;
	public event System.Action<int, bool[]> OnShiftMeasure;
	public event System.Action<int, bool> OnToggleBeatActivity;
	public event System.Action<BeatValue> OnShiftBeatValue;

	public DesignMenuController () {
		measureIndex = 0;
		beatIndex = 0;
		beatValue = BeatValue.Quarter;
	}

	public void LoadBlueprint (BeatmapBlueprint blueprint) {
		this.blueprint = blueprint;
		ShiftMeasure (0);
		ShiftBeatValue (0);
	}

	public void ToggleBeatActivity () {
		measure[beatIndex] = !(measure[beatIndex]);
		if (OnToggleBeatActivity != null)
			OnToggleBeatActivity (beatIndex, measure[beatIndex]);
	}

	public void ShiftMeasure (int shift) {
		measureIndex += shift;
		if (measureIndex < 0)
			measureIndex = 0;
		else if (measureIndex >= blueprint.measures.Count) {
			blueprint.AddMeasure ();
			measureIndex = blueprint.measures.Count - 1;
		}
		measure = blueprint.measures[measureIndex];
		if (OnShiftMeasure != null)
			OnShiftMeasure (measureIndex, measure);
	}

	public void ShiftBeat (int shift) {
		beatIndex += shift * (int)beatValue;
		if (beatIndex < 0)
			beatIndex = 0;
		else if (beatIndex >= measure.Length)
			beatIndex = measure.Length - (int)beatValue;
		if (OnShiftBeat != null)
			OnShiftBeat (beatIndex);
	}

	public void ShiftBeatValue (int shift) {
		List<BeatValue> beatValues = new List<BeatValue> ((BeatValue[])System.Enum.GetValues (typeof(BeatValue)));
		int valueIndex = beatValues.IndexOf (beatValue) + shift;
		if (valueIndex < 0)
			valueIndex = 0;
		else if (valueIndex >= beatValues.Count)
			valueIndex = beatValues.Count - 1;
		beatValue = beatValues[valueIndex];
		beatIndex -= beatIndex % (int)beatValue;
		if (OnShiftBeatValue != null)
			OnShiftBeatValue (beatValue);
		if (OnShiftBeat != null)
			OnShiftBeat (beatIndex);
	}
}
