using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatDetector : MonoBehaviour {

	List <GameObject> currentCollisions = new List <GameObject> ();

	void Start () {

	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.S)) {
			for (int iii = 0; iii < currentCollisions.Count; ++iii) {
				currentCollisions[0].SetActive(false);
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
