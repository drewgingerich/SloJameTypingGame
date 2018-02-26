// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class MeasureInfoViewBehavior : MonoBehaviour {

// 	[SerializeField] Text measureInfoText;

// 	List<bool[]> measures;

// 	public void Load (BeatmapBlueprintDesigner designer, BeatmapBlueprint blueprint) {
// 		measures = blueprint.measures;
// 		designer.OnShiftMeasure += UpdateMeasureInfo;
// 	}

// 	void UpdateMeasureInfo (int measureIndex, bool[] measure) {
// 		measureInfoText.text = string.Format ("{0}/{1}", (measureIndex + 1).ToString (), measures.Count.ToString ());
// 	}
// }
