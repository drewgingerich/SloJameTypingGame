using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareSpawner: MonoBehaviour {

	List <GameObject> pool = new List <GameObject> ();
	[SerializeField] GameObject flarePrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	GameObject GetFlare() {
		GameObject newFlare;
		foreach(GameObject obj in pool) {
			if (!obj.activeInHierarchy) {
				obj.transform.position = gameObject.transform.position;
				obj.SetActive(true);
				return obj;
			}
		}
		newFlare = Instantiate (flarePrefab, gameObject.transform);
		pool.Add (newFlare);
		return newFlare;
	}

	public void SpawnFlare(/*bool GoodOrBad*/) {
		GameObject flare = GetFlare ();
		flare.GetComponent<Flare>().setTrajectory (Vector3.up, 2);
	}
}
