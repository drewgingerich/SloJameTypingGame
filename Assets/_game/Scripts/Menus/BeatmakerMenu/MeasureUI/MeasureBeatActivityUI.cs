using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureBeatActivityUI : MonoBehaviour {

	[SerializeField] RectTransform measureTransform;
	[SerializeField] RectTransform activeArea;
	[SerializeField] GameObject beatMarker;

	List<GameObject> activityMarkers;

	void Awake() {
		activityMarkers = new List<GameObject>(new GameObject[BeatmapBlueprint.measureDivisor]);
	}

	public void LoadMeasure(bool[] measure) {
		for (int i = 0; i < measure.Length; i++) {
			UpdateBeatActivity(i, measure[i]);
		}
	}

	public void UpdateBeatActivity(int index, bool activity) {
		if (activity && activityMarkers[index] == null)
			CreateActivityMarker(index);
		else if (!activity && activityMarkers[index] != null)
			DestroyActivityMarker(index);
	}

	void CreateActivityMarker(int index) {
		GameObject newMarker = GameObject.Instantiate(beatMarker, measureTransform);
		RectTransform rectTransform = (RectTransform)newMarker.transform;
		rectTransform.localPosition = new Vector3(index - 96f, 0f, 0f);
		activityMarkers[index] = newMarker;
	}

	void DestroyActivityMarker(int index) {
		if (activityMarkers[index] != null)
			Destroy(activityMarkers[index]);
		activityMarkers[index] = null;
	}
}
