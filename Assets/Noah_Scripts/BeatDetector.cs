using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatDetector : MonoBehaviour {

	List <GameObject> currentCollisions = new List <GameObject> ();
	[SerializeField] FlareSpawner listener;

	void Start () {

	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.S)) {
			//assume that Drew's Text thing has a getCurrentLetter() function;
			bool triggered = false;

			for (int iii = 0; iii < currentCollisions.Count; ++iii) {
				currentCollisions [0].SetActive (false);
				listener.SpawnFlare (true);
				triggered = true;
			}
			if (!triggered) {
				listener.SpawnFlare (false);
			}
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		currentCollisions.Add(col.gameObject);
	}

	void OnTriggerExit2D (Collider2D col) {
		currentCollisions.Remove(col.gameObject);
	}
}
