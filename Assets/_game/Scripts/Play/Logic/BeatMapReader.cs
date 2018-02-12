using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMapReader {

	public event System.Action<float> OnReadBeat = delegate {};
	public event System.Action OnFinishBeatMap = delegate {};

	List<float> beatMap;
	int mapIndex;

	public BeatMapReader (List<float> beatMap) {
		this.beatMap = beatMap;
	}

	// public List<float> ReadMapToBeatCount (float count) {
	// 	List<float> beatsToSpawn = new List<float> ();
	// 	if (mapIndex >= beatMap.Count)
	// 		return beatsToSpawn;
	// 	while (count >= beatMap[mapIndex]) {
	// 		beatsToSpawn.Add (beatMap[mapIndex]);
	// 		mapIndex++;
	// 		if (mapIndex >= beatMap.Count)
	// 			OnFinishBeatMap ();
	// 	}
	// 	return beatsToSpawn;
	// }

	public bool SearchForNextBeat (float count) {
		if (mapIndex >= beatMap.Count)
			return false;
		if (count < beatMap[mapIndex])
			return false;
		OnReadBeat (beatMap[mapIndex]);
		mapIndex++;
		if (mapIndex >= beatMap.Count)
			OnFinishBeatMap ();
		return true;
	}
}
