using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatIndicator : MonoBehaviour {

	Vector3 direction;
	float speed;

	void Update () {
		gameObject.transform.Translate (direction * speed * Time.deltaTime);
	}

	void OnTriggerExit2D (Collider2D other) {
		gameObject.SetActive (false);
	}

	public void setTrajectory(Vector3 direction, float speed) {
		this.direction = direction;
		this.speed = speed;
	}


}
