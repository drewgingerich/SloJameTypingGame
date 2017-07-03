using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatIndicatorSpawner : MonoBehaviour {

	[SerializeField] SongManager songManager;
	[SerializeField] RhythmSettings rhythmSettings;
	[SerializeField] FutureBeatReader futureBeatReader;
	[SerializeField] GameObject targetObject;
	[SerializeField] GameObject indicatorPrefab;

	Vector3 spawnPosition;
	Vector3 targetPosition;
	List<GameObject> pool;

	void Start () {
		pool = new List<GameObject> ();
		spawnPosition = gameObject.transform.position;
		targetPosition = targetObject.transform.position;
		futureBeatReader.OnFutureBeat += SpawnIndicator;
	}

	public void SpawnIndicator() {
		BeatIndicator beatIndicator = GetIndicator ().GetComponent<BeatIndicator> ();
		beatIndicator.setTrajectory (spawnPosition, targetPosition, rhythmSettings.indicatorTravelTime);
	}

	GameObject GetIndicator() {
		// Check for idle indicator objects.
		foreach (GameObject indicatorObj in pool) {
			if (!indicatorObj.activeInHierarchy) {
				indicatorObj.transform.position = spawnPosition;
				indicatorObj.SetActive (true);
				return indicatorObj;
			}
		}
		// Make new indicator object if necessary.
		GameObject newIndicatorObj = Instantiate (indicatorPrefab, gameObject.transform);
		newIndicatorObj.GetComponent<BeatIndicator> ().SetSongManager (songManager);
		pool.Add (newIndicatorObj);
		return newIndicatorObj;
	}
}
