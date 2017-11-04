using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorSpawnerBehavior : MonoBehaviour {

	[SerializeField] GameObject indicatorPrefab;
	[SerializeField] Vector3 indicatorSpawn;
	[SerializeField] Vector3 indicatorTarget;

	public void Wire (BeatSpawner spawner) {
		spawner.OnSpawnBeat += SpawnIndicator;
	}

	void SpawnIndicator (Beat beat) {
		GameObject newIndicatorObject = Instantiate (indicatorPrefab);
		IndicatorBehavior newIndicatorBehavior = newIndicatorObject.GetComponent<IndicatorBehavior> ();
		newIndicatorBehavior.Wire (beat, indicatorSpawn, indicatorTarget);
	}
}
