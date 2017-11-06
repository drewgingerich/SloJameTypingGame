using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorSpawnerBehavior : MonoBehaviour {

	[SerializeField] GameObject indicatorPrefab;
	[SerializeField] Transform indicatorSpawn;
	[SerializeField] Transform indicatorTarget;

	public void Wire (BeatSpawner spawner) {
		spawner.OnSpawnBeat += SpawnIndicator;
	}

	void SpawnIndicator (Beat beat) {
		GameObject newIndicatorObject = Instantiate (indicatorPrefab);
		newIndicatorObject.transform.parent = gameObject.transform;
		IndicatorBehavior newIndicatorBehavior = newIndicatorObject.GetComponent<IndicatorBehavior> ();
		newIndicatorBehavior.Wire (beat, indicatorSpawn.position, indicatorTarget.position);
	}
}
