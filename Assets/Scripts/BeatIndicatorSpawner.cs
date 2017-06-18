using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatIndicatorSpawner : MonoBehaviour {

	// Dealt with in inspector.
	[SerializeField] GameObject indicatorPrefab;
	[SerializeField] float indicatorSpeed;

	Vector3 spawnPosition;
	List<GameObject> pool;

	void Start () {
		pool = new List<GameObject> ();
		spawnPosition = gameObject.transform.position;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			SpawnIndicator ();
		}
	}

	GameObject GetIndicator() {
		GameObject newIndicatorObj;
		// Check if there is an existing, idle GameObject in the pool.
		foreach (GameObject obj in pool) {
			if (!obj.activeInHierarchy) {
				obj.transform.position = spawnPosition;
				obj.SetActive (true);
				obj.transform.position = spawnPosition;
				return obj;
			}
		}
		// If no existing GameObject is free, create a new one.
		newIndicatorObj = Instantiate (indicatorPrefab, gameObject.transform);
		pool.Add (newIndicatorObj);
		return newIndicatorObj;
	}

	public void SpawnIndicator() {
		GameObject indicatorObj = GetIndicator ();
		indicatorObj.GetComponent<BeatIndicator>().setTrajectory (Vector3.left, indicatorSpeed);
	}
}
