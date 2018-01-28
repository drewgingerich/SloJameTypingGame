using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorSpawner : MonoBehaviour {

	[SerializeField] GameObject indicatorPrefab;
	[SerializeField] Transform indicatorSpawn;
	[SerializeField] Transform indicatorTarget;

	public void Wire (BeatSpawner spawner) {
		spawner.OnSpawnBeat += SpawnIndicator;
	}

	public void SpawnIndicator (Beat beat) {
		GameObject newIndicatorObject = Instantiate (indicatorPrefab);
		newIndicatorObject.transform.parent = gameObject.transform;
		Indicator newIndicatorBehavior = newIndicatorObject.GetComponent<Indicator> ();
		newIndicatorBehavior.Wire (beat, indicatorSpawn.position, indicatorTarget.position);
	}
}
