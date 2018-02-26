﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAutoPrepare : MonoBehaviour {

	void Start() {
		RectTransform rectTransform = GetComponent<RectTransform>();
		rectTransform.offsetMax = rectTransform.offsetMin = Vector2.zero;
		gameObject.SetActive(false);
	}
}
