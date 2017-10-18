using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BeatMap {

	public enum Difficulty { Easy, Medium, Hard }

	public Difficulty difficulty;
	public List<bool[]> measureMaps;

	public BeatMap () {
		measureMaps = new List<bool[]> ();
		measureMaps.Add (new bool [192]);
	}
}
