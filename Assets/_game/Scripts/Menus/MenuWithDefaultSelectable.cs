using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuWithDefaultSelectable : MonoBehaviour {

	public Selectable defaultSelectable;

	void OnEnable() {
		defaultSelectable.Select();
	}
}
