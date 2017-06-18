using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatIndicatorSpawner : MonoBehaviour {

	Vector3 spawnPosition = new Vector2(0, 0);
	List<BeatIndicator> pool;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void getIndicator() {
		foreach (BeatIndicator indicator in pool) {
			if (indicator.gameObject.activeInHierarchy) {
				indicator.gameObject.transform.position = spawnPosition;
				indicator.gameObject.SetActive (true);
				return indicator;
			}
		}
		GameObject newIndicator = new GameObject ();
		newIndicator.AddComponent (new BeatIndicator ());
		pool.Add (newIndicator);
		return newIndicator;
	}

	void spawnIndicator() {
		BeatIndicator indicator = getIndicator ();
		indicator.move (new Vector2 (-1, 0), 1);
	}
}
