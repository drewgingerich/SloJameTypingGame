using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BeatSpawner {

	public event System.Action<Beat> OnSpawnBeat = delegate {};

	float spawnCountOffset = 5f;
	BeatMapReader beatMapReader;
	TextReader textReader;
	bool doneSpawning = false;

	public BeatSpawner (BeatMapReader beatMapReader, TextReader textReader) {
		this.beatMapReader = beatMapReader;
		beatMapReader.OnReadBeat += SpawnBeat;
		beatMapReader.OnFinishBeatMap += FinishSpawning;
		this.textReader = textReader;
		textReader.OnFinishText += FinishSpawning;
	}

	public void CheckForBeatSpawns (float currentCounts) {
		while (!doneSpawning) {
			bool beatSpawned = beatMapReader.SearchForNextBeat (currentCounts + spawnCountOffset);
			if (!beatSpawned)
				break;
		}
	}

	void SpawnBeat (float targetCounts) {
		char nextChar = textReader.GetNextChar ();
		Beat newBeat = new Beat(targetCounts - spawnCountOffset, targetCounts, nextChar);
		OnSpawnBeat(newBeat);	
	}

	void FinishSpawning () {
		doneSpawning = true;
	}
}
