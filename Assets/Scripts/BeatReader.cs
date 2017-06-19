using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatReader : MonoBehaviour {

	// Set in inspector. 
	[SerializeField] GameObject spawnerObj;
	[SerializeField] SongBeats songBeats;

	float timeSinceLastBeat;
	int beatIndex;

	BeatIndicatorSpawner spawner;

	void Start () {
		spawner = spawnerObj.GetComponent<BeatIndicatorSpawner> ();
	}

	void FixedUpdate () {
		timeSinceLastBeat += Time.deltaTime;
		if (timeSinceLastBeat >= songBeats.beatOffsets [beatIndex]) {
			spawner.SpawnIndicator ();
			timeSinceLastBeat -= songBeats.beatOffsets[beatIndex];
			beatIndex = (beatIndex + 1) % songBeats.beatOffsets.Count;
		}
	}
}
