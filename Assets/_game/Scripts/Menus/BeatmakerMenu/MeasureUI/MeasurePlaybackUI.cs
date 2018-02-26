using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasurePlaybackUI : MonoBehaviour {

	[SerializeField] RectTransform measureTransform;
	[SerializeField] GameObject playheadMarker;

	public void UpdatePlayheadMarker(float progress) {
		float xPosition = measureTransform.rect.width * progress - measureTransform.rect.width / 2;
		playheadMarker.transform.localPosition = new Vector3(xPosition, 0f, 0f);
	}
}
