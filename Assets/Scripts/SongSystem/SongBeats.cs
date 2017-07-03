using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongBeats : ScriptableObject {

	public enum Difficulty { Easy, Medium, Hard };

	public List<float> beatOffsets;
	[System.NonSerialized] public float beatsPerMeasure;
	[System.NonSerialized] public Difficulty difficulty;
}
