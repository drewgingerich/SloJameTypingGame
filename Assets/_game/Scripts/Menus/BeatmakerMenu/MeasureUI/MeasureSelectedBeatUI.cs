using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureSelectedBeatUI : MonoBehaviour {

	[SerializeField] RectTransform placeholderMarker;

	public void UpdateCurrentBeat(int index) {
		placeholderMarker.localPosition = new Vector3(index - 96f, 0f, 0f);
	}
}
