using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorSpawner : MonoBehaviour {

	[SerializeField] GameObject indicatorPrefab;
	[SerializeField] Transform indicatorSpawn;
	[SerializeField] Transform indicatorTarget;

	void Start () {
		indicatorSpawn.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0.1f);
		indicatorTarget.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.1f);
	}

	public void SpawnIndicator (Beat beat) {
		GameObject newIndicatorObject = Instantiate (indicatorPrefab);
		newIndicatorObject.transform.parent = gameObject.transform;
		Indicator newIndicatorBehavior = newIndicatorObject.GetComponent<Indicator> ();
		newIndicatorBehavior.Wire (beat, indicatorSpawn.position, indicatorTarget.position);
	}
}
