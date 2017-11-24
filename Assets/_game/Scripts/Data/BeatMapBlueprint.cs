using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMapBlueprint {

	public const int measureDivisor = 192;

	public List<bool[]> measures;

	public BeatMapBlueprint () {
		measures = new List<bool[]> ();
	}

	public void AddMeasure () {
		measures.Add (new bool [measureDivisor]);
	}
}
