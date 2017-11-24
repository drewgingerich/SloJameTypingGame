using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureViewBehavior : MonoBehaviour {

	[SerializeField] RectTransform measureTransform;
	[SerializeField] RectTransform activeArea;
	[SerializeField] RectTransform placeholderMarker;
	[SerializeField] GameObject beatMarker;
	[SerializeField] GameObject playheadMarker;

	DesignMenuController designer;

	List<GameObject> activityMarkers;

	public void Wire (DesignMenuController designer, MeasureAudioController audioController) {
		this.designer = designer;
		designer.OnShiftBeat += UpdateCurrentBeat;
		designer.OnToggleBeatActivity += UpdateBeatActivity;
		designer.OnShiftMeasure += LoadMeasure;
		audioController.OnUpdateSectionProgress += UpdatePlayheadMarker;
		audioController.OnEndSection += () => UpdatePlayheadMarker (0f);
	}

	void Awake () {
		activityMarkers = new List<GameObject> (new GameObject[BeatMapBlueprint.measureDivisor]);
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftArrow)) 
			designer.ShiftBeat (-1);
		else if (Input.GetKeyDown (KeyCode.RightArrow))
			designer.ShiftBeat (1);
		else if (Input.GetKeyDown (KeyCode.UpArrow))
			designer.ShiftBeatValue (1);
		else if (Input.GetKeyDown (KeyCode.DownArrow))
			designer.ShiftBeatValue (-1);
		else if (Input.GetKeyDown (KeyCode.Space))
			designer.ToggleBeatActivity ();
		else if (Input.GetKeyDown (KeyCode.Period))
			designer.ShiftMeasure (1);
		else if (Input.GetKeyDown (KeyCode.Comma))
			designer.ShiftMeasure (-1);
	}

	public void UpdateCurrentBeat (int index) {
		placeholderMarker.localPosition = new Vector3 (index - 96f, 0f, 0f);
	}

	void LoadMeasure (int measureIndex, bool[] measure) {
		for (int i = 0; i < measure.Length; i++) {
			UpdateBeatActivity (i, measure[i]);
		}
	}

	void UpdateBeatActivity (int index, bool activity) {
		if (activity && activityMarkers [index] == null)
			CreateActivityMarker (index);
		else if (!activity && activityMarkers [index] != null)
			DestroyActivityMarker (index);
	}

	void CreateActivityMarker (int index) {
		GameObject newMarker = GameObject.Instantiate (beatMarker, measureTransform);
		RectTransform rectTransform = (RectTransform)newMarker.transform;
		rectTransform.localPosition = new Vector3 (index - 96f, 0f, 0f);
		activityMarkers [index] = newMarker;
	}

	void DestroyActivityMarker (int index) {
		if (activityMarkers[index] != null)
			Destroy (activityMarkers[index]);
		activityMarkers [index] = null;
	}

	void UpdatePlayheadMarker (float progress) {
		float xPosition = measureTransform.rect.width * progress - measureTransform.rect.width / 2;
		playheadMarker.transform.localPosition = new Vector3 (xPosition, 0f, 0f);
	}
}
