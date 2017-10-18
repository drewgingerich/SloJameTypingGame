using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorSpawnerBehavior : MonoBehaviour {

	[SerializeField] PlaySettings playSettings;
	[SerializeField] InteractiveBeatSystemBehavior interactiveBeatSystem;
	[SerializeField] GameObject targetObject;
	[SerializeField] GameObject indicatorPrefab;

	List<GameObject> pool;

	void Start () {
		pool = new List<GameObject> ();
		interactiveBeatSystem.BeatSpawner.OnSpawnBeat += SpawnIndicator;
	}

	void SpawnIndicator(InteractiveBeat interactiveBeat) {
		IndicatorBehavior beatIndicator = GetIndicator ();
		beatIndicator.setTrajectory (transform.position, targetObject.transform.position);
		beatIndicator.setTrackedBeat (interactiveBeat);
	}

	IndicatorBehavior GetIndicator() {
		IndicatorBehavior indicator = null;
		foreach (GameObject indicatorObj in pool) {
			if (!indicatorObj.activeInHierarchy) {
				indicatorObj.transform.position = transform.position;
				indicatorObj.SetActive (true);
				indicator = indicatorObj.GetComponent<IndicatorBehavior> ();
			}
		}
		if (indicator == null) {
			GameObject newIndicatorObj = Instantiate (indicatorPrefab, gameObject.transform);
			indicator = newIndicatorObj.GetComponent<IndicatorBehavior> ();
			pool.Add (newIndicatorObj);
		}
		return indicator;
	}
}
