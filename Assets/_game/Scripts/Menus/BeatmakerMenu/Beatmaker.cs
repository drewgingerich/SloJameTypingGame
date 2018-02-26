using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class BeatmakerChangeBeatValueEvent : UnityEvent<int> { };
[System.Serializable] public class BeatmakerChangeBeatIndexEvent : UnityEvent<int> { };
[System.Serializable] public class BeatmakerToggleBeatActivityEvent : UnityEvent<int, bool> { };
[System.Serializable] public class BeatmakerChangeMeasureIndexEvent : UnityEvent<int> { };
[System.Serializable] public class BeatmakerChangeMeasureEvent : UnityEvent<bool[]> { };

public class Beatmaker : MonoBehaviour {

	public BeatmakerChangeBeatValueEvent OnChangeBeatValue;
	public BeatmakerChangeBeatIndexEvent OnChangeBeatIndex;
	public BeatmakerToggleBeatActivityEvent OnToggleBeatActivity;
	public BeatmakerChangeMeasureIndexEvent OnChangeMeasureIndex;
	public BeatmakerChangeMeasureEvent OnChangeMeasure;

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

	public void SaveChanges() {
		DataNavigator.currentSong.Save();
	}

	void OnEnable() {
		measureIndex = 0;
		beatIndex = 0;
		beatValue = BeatValue.Quarter;
		blueprint = DataNavigator.GetCurrentBlueprint();
		ShiftMeasure(0);
		ShiftBeatValue(0);
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			ShiftBeat(-1);
		else if (Input.GetKeyDown(KeyCode.RightArrow))
			ShiftBeat(1);
		else if (Input.GetKeyDown(KeyCode.UpArrow))
			ShiftBeatValue(1);
		else if (Input.GetKeyDown(KeyCode.DownArrow))
			ShiftBeatValue(-1);
		else if (Input.GetKeyDown(KeyCode.Space))
			ToggleBeatActivity();
		else if (Input.GetKeyDown(KeyCode.Period))
			ShiftMeasure(1);
		else if (Input.GetKeyDown(KeyCode.Comma))
			ShiftMeasure(-1);
	}

	void ToggleBeatActivity() {
		measure[beatIndex] = !(measure[beatIndex]);
		if (OnToggleBeatActivity != null)
			OnToggleBeatActivity.Invoke(beatIndex, measure[beatIndex]);
	}

	void ShiftMeasure(int shift) {
		measureIndex += shift;
		if (measureIndex < 0)
			measureIndex = 0;
		else if (measureIndex >= blueprint.measures.Count) {
			blueprint.AddMeasure();
			measureIndex = blueprint.measures.Count - 1;
		}
		measure = blueprint.measures[measureIndex];
		if (OnChangeMeasureIndex != null) {
			OnChangeMeasureIndex.Invoke(measureIndex);
			OnChangeMeasure.Invoke(measure);
		}
	}

	void ShiftBeat(int shift) {
		beatIndex += shift * (int)beatValue;
		if (beatIndex < 0)
			beatIndex = 0;
		else if (beatIndex >= measure.Length)
			beatIndex = measure.Length - (int)beatValue;
		if (OnChangeBeatIndex != null)
			OnChangeBeatIndex.Invoke(beatIndex);
	}

	void ShiftBeatValue(int shift) {
		List<BeatValue> beatValues = new List<BeatValue>((BeatValue[])System.Enum.GetValues(typeof(BeatValue)));
		int valueIndex = beatValues.IndexOf(beatValue) + shift;
		if (valueIndex < 0)
			valueIndex = 0;
		else if (valueIndex >= beatValues.Count)
			valueIndex = beatValues.Count - 1;
		beatValue = beatValues[valueIndex];
		beatIndex -= beatIndex % (int)beatValue;
		if (OnChangeBeatValue != null)
			OnChangeBeatValue.Invoke((int)beatValue);
		if (OnChangeBeatIndex != null)
			OnChangeBeatIndex.Invoke(beatIndex);
	}
}
