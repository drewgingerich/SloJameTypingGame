using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapMenu : MonoBehaviour {

	void Awake () {
		gameObject.SetActive(false);
		RectTransform rectTransform = GetComponent<RectTransform>();
		rectTransform.offsetMax = rectTransform.offsetMin = Vector2.zero;
	}
}
