using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : MonoBehaviour {

	Vector3 direction;
	float speed;

	void FixedUpdate () {
		gameObject.transform.Translate (direction * speed * Time.deltaTime);
	}


	public void setTrajectory(Vector3 direction, float speed) {
		this.direction = direction;
		this.speed = speed;
		this.transform.position = new Vector3(0,0,0);
	}
}
