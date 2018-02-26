using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeasureInfoUI : MonoBehaviour {

	[SerializeField] Text measureInfoText;

	BeatmapBlueprint blueprint;
	int measureCount;
	int measureIndex;

	void OnEnable() {
		if (blueprint != null)
			blueprint.OnChangeMeasureCount -= UpdateMeasureCount;
		blueprint = DataNavigator.GetCurrentBlueprint();
		blueprint.OnChangeMeasureCount += UpdateMeasureCount;
		measureCount = blueprint.measures.Count;
	}

	public void UpdateMeasureCount(int newCount) {
		measureCount = newCount;
		DisplayMeasureInfo();
	}

	public void UpdateMeasureIndex(int newIndex) {
		measureIndex = newIndex;
		DisplayMeasureInfo();
	}

	void DisplayMeasureInfo() {
		measureInfoText.text = string.Format("{0}/{1}", (measureIndex + 1).ToString(), measureCount.ToString());
	}
}
