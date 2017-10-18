using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeasureMap {

	public bool[] beatFlags;

	public MeasureMap () {
		beatFlags = new bool[192];
	}
}
