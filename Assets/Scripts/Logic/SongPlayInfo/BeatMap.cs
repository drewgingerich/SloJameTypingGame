using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BeatMap {

	public enum Difficulty { Easy, Medium, Hard }

	Difficulty difficulty;
	List<MeasureMap> measureMaps;
}
