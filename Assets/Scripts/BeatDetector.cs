using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
		
	void OnTriggerEnter2D (Collider2D other) {
		if (Input.GetKey (KeyCode.S)) {
			other.gameObject.SetActive (false);
			Debug.Log ("TEST");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
