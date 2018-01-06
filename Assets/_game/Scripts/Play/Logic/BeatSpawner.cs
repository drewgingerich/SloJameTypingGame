using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner {

	public event System.Action<Beat> OnSpawnBeat = delegate {};

	public BeatSpawner (BeatMapReader beatMapReader) {
		beatMapReader.OnReadBeat += SpawnBeat;
	}

	void SpawnBeat (float spawnTime, float targetTime, int textIndex) {
		Beat newBeat = new Beat (spawnTime, targetTime, textIndex);
		OnSpawnBeat (newBeat);
	}
}
