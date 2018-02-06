using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BeatSpawner {

	public event System.Action<Beat> OnSpawnBeat = delegate {};

	public BeatSpawner (BeatMapReader beatMapReader) {
		beatMapReader.OnReadBeat += SpawnBeat;
	}

	void SpawnBeat (float spawnTime, float targetTime, char textChar) {
		Beat newBeat = new Beat (spawnTime, targetTime, textChar);
		OnSpawnBeat (newBeat);
	}
}
